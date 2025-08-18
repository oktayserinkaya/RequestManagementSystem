using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM;
using RequestEntity = CORE.Entities.Concrete.Request;

namespace WEB.Areas.PaymentTransaction.Controllers
{
    [Area("PaymentTransaction")]
    [Authorize(Roles = "OdemeBirimi,Admin")]
    [Route("PaymentTransaction/[controller]")]
    public class PaymentTransactionController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly IPurchaseManager _purchaseManager;
        private readonly ILogger<PaymentTransactionController> _logger;
        private readonly IWebHostEnvironment _env;

        public PaymentTransactionController(
            IRequestManager requestManager,
            IPurchaseManager purchaseManager,
            ILogger<PaymentTransactionController> logger,
            IWebHostEnvironment env)
        {
            _requestManager = requestManager;
            _purchaseManager = purchaseManager;
            _logger = logger;
            _env = env;
        }

        // Ödeme bekleyenler
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(string? q = null, int take = 100)
        {
            var list = await _requestManager.GetFilteredListAsync(
                select: x => new PaymentTransactionListVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,
                    CreatedDate = x.CreatedDate,
                    EmployeeFullName = x.Employee != null ? (x.Employee.FirstName + " " + x.Employee.LastName) : string.Empty,
                    EmployeeEmail = x.Employee != null ? (x.Employee.Email ?? string.Empty) : string.Empty,
                    DepartmentName = (x.Employee != null && x.Employee.Department != null)
                        ? x.Employee.Department.DepartmentName ?? string.Empty : string.Empty,
                    CategoryName = (x.Product != null && x.Product.SubCategory != null && x.Product.SubCategory.Category != null)
                        ? (x.Product.SubCategory.Category.CategoryName ?? string.Empty) : string.Empty,
                    SubCategoryName = (x.Product != null && x.Product.SubCategory != null)
                        ? (x.Product.SubCategory.SubCategoryName ?? string.Empty) : string.Empty,
                    ProductName = (x.Product != null && !string.IsNullOrWhiteSpace(x.Product.ProductName))
                        ? x.Product.ProductName! : (x.SpecialProductName ?? "-"),
                    Amount = x.Amount,
                    SpecPath = x.ProductFeaturesFilePath
                },
                where: x =>
                    x.Status == Status.WaitingPayment &&
                    (string.IsNullOrWhiteSpace(q) ||
                     ((x.Employee!.FirstName + " " + x.Employee.LastName).ToLower().Contains(q!.ToLower())) ||
                     ((x.Employee.Email ?? string.Empty).ToLower().Contains(q!.ToLower())) ||
                     ((x.Employee.Department!.DepartmentName ?? string.Empty).ToLower().Contains(q!.ToLower()))),
                orderBy: qy => qy.OrderByDescending(z => z.CreatedDate),
                join: qy => qy
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
            );

            return View(list.Take(take).ToList());
        }

        // Şartname görüntüle
        [HttpGet("Spec/{id:guid}")]
        public async Task<IActionResult> Spec(Guid id)
        {
            var req = await _requestManager.GetByIdAsync<RequestEntity>(id);
            if (req is null) return NotFound("Talep bulunamadı.");

            var rawPath = req.ProductFeaturesFilePath;
            if (string.IsNullOrWhiteSpace(rawPath))
                return NotFound("Şartname dosyası kayıtlı değil.");

            if (rawPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return Redirect(rawPath);

            var candidates = new List<string>();
            string p = rawPath.Replace('\\', '/');

            if (p.StartsWith("~/")) p = p[2..];
            if (p.StartsWith("/")) p = p[1..];
            if (!string.IsNullOrWhiteSpace(p))
                candidates.Add(Path.Combine(_env.WebRootPath ?? "", p.Replace('/', Path.DirectorySeparatorChar)));

            if (!p.StartsWith("uploads/", StringComparison.OrdinalIgnoreCase) &&
                !p.StartsWith("upload/", StringComparison.OrdinalIgnoreCase))
            {
                candidates.Add(Path.Combine(_env.WebRootPath ?? "",
                    Path.Combine("uploads", p).Replace('/', Path.DirectorySeparatorChar)));
            }

            candidates.Add(rawPath);
            candidates.Add(Path.Combine(_env.ContentRootPath, "uploads", Path.GetFileName(rawPath)));

            string? found = candidates.FirstOrDefault(System.IO.File.Exists);
            if (found == null)
            {
                _logger.LogWarning("Spec not found. raw='{raw}', tried={@cands}", rawPath, candidates);
                return NotFound("Şartname dosyası bulunamadı.");
            }

            var fileName = Path.GetFileName(found);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileName}\"";
            return PhysicalFile(found, "application/pdf", enableRangeProcessing: true);
        }
        // ÖDEME FORMU (GET)
        [HttpGet("Pay/{id:guid}")]
        public async Task<IActionResult> Pay(Guid id)
        {
            var req = (await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id,
                join: q => q
                    .Include(r => r.Employee)!.ThenInclude(e => e!.Department!)
                    .Include(r => r.Product!)
                    .Include(r => r.Purchase!)
            )).FirstOrDefault();

            if (req is null)
            {
                TempData["Error"] = "Talep bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var purchase = await _purchaseManager.GetForPaymentAsync(id);

            decimal qty = purchase?.Quantity ?? 0m;
            decimal unit = purchase?.UnitPrice ?? 0m;
            decimal subtotal = qty * unit;
            decimal discRate = purchase?.DiscountRate ?? 0m;
            decimal vatRate = purchase?.VatRate ?? 0m;
            decimal discAmt = subtotal * discRate / 100m;
            decimal net = subtotal - discAmt;
            decimal vatAmt = net * vatRate / 100m;
            decimal grand = purchase?.GrandTotal ?? (net + vatAmt);

            var vm = new PaymentFormVM
            {
                // Talep özeti
                RequestId = req.Id,
                RequestDate = req.RequestDate ?? req.CreatedDate,
                CreatedDate = req.CreatedDate,
                EmployeeFullName = req.Employee != null ? $"{req.Employee.FirstName} {req.Employee.LastName}" : "-",
                DepartmentName = req.Employee?.Department?.DepartmentName ?? "-",

                // Ürün bilgileri
                ProductName = req.Product?.ProductName ?? "-",   // katalog adı
                SpecialProductName = req.SpecialProductName,            // <-- EKLENDİ
                RequestedAmount = req.Amount,
                SpecPath = req.ProductFeaturesFilePath,

                // Tedarikçi (satın alma)
                SupplierName = purchase?.SupplierName,
                SupplierTaxNo = purchase?.SupplierTaxNo,
                SupplierIban = purchase?.SupplierIban,
                SupplierEmail = purchase?.SupplierEmail,
                SupplierPhone = purchase?.SupplierPhone,

                // Teklif meta (varsa)
                OfferNo = purchase?.OfferNo,
                OfferDate = purchase?.OfferDate,
                PaymentTerms = purchase?.PaymentTerms,
                Notes = purchase?.Notes,

                // Fiyatlandırma
                Quantity = purchase?.Quantity,
                UnitPrice = purchase?.UnitPrice,
                DiscountRate = discRate,
                VatRate = vatRate,
                Subtotal = purchase?.Subtotal ?? subtotal,
                DiscountAmount = purchase?.DiscountAmount ?? discAmt,
                VatAmount = purchase?.VatAmount ?? vatAmt,
                GrandTotal = grand,
                Currency = purchase?.Currency ?? "TRY",

                // Ödeme varsayılanları
                PaymentMethod = "EFT/Havale",
                PaymentDate = DateTime.Today,
                PaymentAmount = grand,

                // Fatura tarihi varsayılanı
                InvoiceDate = DateTime.Today
            };

            return View(vm);
        }


        // ÖDEMEYİ İŞLE + PDF ÜRET (POST)
        [HttpPost("Pay/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(Guid id, PaymentFormVM model)
        {
            if (id != model.RequestId)
                ModelState.AddModelError("", "Talep uyumsuz.");

            if (model.PaymentAmount <= 0)
                ModelState.AddModelError(nameof(model.PaymentAmount), "Ödeme tutarı giriniz.");

            if (!ModelState.IsValid)
                return View(model);

            var req = (await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id,
                join: q => q
                    .Include(r => r.Employee)!.ThenInclude(e => e!.Department!)
                    .Include(r => r.Product!)
            )).FirstOrDefault();

            if (req is null)
            {
                TempData["Error"] = "Talep bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var purchase = await _purchaseManager.GetForPaymentAsync(id);

            // --- ÖZETLER BOŞ KALMASIN ---
            if (!model.RequestDate.HasValue)
                model.RequestDate = req.RequestDate ?? req.CreatedDate;

            if (model.CreatedDate == default)
                model.CreatedDate = req.CreatedDate;

            if (string.IsNullOrWhiteSpace(model.EmployeeFullName))
                model.EmployeeFullName = req.Employee != null ? $"{req.Employee.FirstName} {req.Employee.LastName}" : "-";

            if (string.IsNullOrWhiteSpace(model.DepartmentName))
                model.DepartmentName = req.Employee?.Department?.DepartmentName ?? "-";

            // Ürün alanları
            if (string.IsNullOrWhiteSpace(model.ProductName))
                model.ProductName = req.Product?.ProductName ?? "-";

            if (string.IsNullOrWhiteSpace(model.SpecialProductName))
                model.SpecialProductName = req.SpecialProductName;   // <-- EKLENDİ

            if (model.RequestedAmount == null)
                model.RequestedAmount = req.Amount;

            if (!model.InvoiceDate.HasValue)
                model.InvoiceDate = DateTime.Today;

            // Tedarikçi alanları
            model.SupplierName ??= purchase?.SupplierName;
            model.SupplierTaxNo ??= purchase?.SupplierTaxNo;
            model.SupplierIban ??= purchase?.SupplierIban;
            model.SupplierEmail ??= purchase?.SupplierEmail;
            model.SupplierPhone ??= purchase?.SupplierPhone;

            var ok = await _requestManager.UpdateAsync(new UpdateRequestDTO
            {
                Status = Status.Paid,
                UpdatedDate = DateTime.UtcNow
            }, id);

            if (!ok)
            {
                TempData["Error"] = "Talep güncellenemedi (ödeme işaretlenemedi).";
                return View(model);
            }

            var pdfBytes = GeneratePaymentPdf(model); // PDF içinde SpecialProductName kullanmak isterseniz bu fonksiyonda da yazdırabilirsiniz.

            var root = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var dir = Path.Combine(root, "uploads", "payments");
            Directory.CreateDirectory(dir);
            var fileName = $"Payment_{model.RequestId:N}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            System.IO.File.WriteAllBytes(Path.Combine(dir, fileName), pdfBytes);

            TempData["Success"] = "Ödeme kaydedildi ve PDF oluşturuldu.";
            return File(pdfBytes, "application/pdf", fileName);
        }



        // --- PDF ÜRETİCİ ---
        private byte[] GeneratePaymentPdf(PaymentFormVM m)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Text("ÖDEME DEKONTU").Bold().FontSize(18);
                        row.ConstantItem(180).AlignRight().Text($"Tarih: {DateTime.Now:dd.MM.yyyy}");
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // Talep özeti
                        col.Item().Text("Talep Özeti").Bold().FontSize(13);
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.ConstantColumn(140); c.RelativeColumn();
                                c.ConstantColumn(140); c.RelativeColumn();
                            });

                            static IContainer L(IContainer c) => c.PaddingVertical(4).Background(Colors.Grey.Lighten3).PaddingLeft(6);
                            static IContainer V(IContainer c) => c.PaddingVertical(4).PaddingLeft(6);
                            void Row(string l, string v) { t.Cell().Element(L).Text(l); t.Cell().Element(V).Text(v); }

                            Row("Talep No", m.RequestId.ToString());
                            Row("Talep Tarihi", (m.RequestDate ?? m.CreatedDate).ToString("dd.MM.yyyy"));
                            Row("Personel", m.EmployeeFullName ?? "-");
                            Row("Departman", m.DepartmentName ?? "-");
                            Row("Ürün", m.ProductName ?? "-");
                            Row("Talep Adedi", m.RequestedAmount?.ToString() ?? "-");
                        });

                        // Tedarikçi
                        col.Item().PaddingTop(10).Text("Tedarikçi Bilgileri").Bold().FontSize(13);
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.ConstantColumn(140); c.RelativeColumn(); c.ConstantColumn(140); c.RelativeColumn(); });
                            static IContainer L(IContainer c) => c.PaddingVertical(4).Background(Colors.Grey.Lighten3).PaddingLeft(6);
                            static IContainer V(IContainer c) => c.PaddingVertical(4).PaddingLeft(6);
                            void Row(string l, string v) { t.Cell().Element(L).Text(l); t.Cell().Element(V).Text(v); }

                            Row("Tedarikçi", m.SupplierName);
                            Row("Vergi No", m.SupplierTaxNo ?? "-");
                            Row("IBAN", m.SupplierIban ?? "-");
                            Row("E-Posta", m.SupplierEmail ?? "-");
                            Row("Telefon", m.SupplierPhone ?? "-");
                        });

                        // Fatura / Ödeme
                        col.Item().PaddingTop(10).Text("Fatura / Ödeme").Bold().FontSize(13);
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.ConstantColumn(140); c.RelativeColumn(); c.ConstantColumn(140); c.RelativeColumn(); });
                            static IContainer L(IContainer c) => c.PaddingVertical(4).Background(Colors.Grey.Lighten3).PaddingLeft(6);
                            static IContainer V(IContainer c) => c.PaddingVertical(4).PaddingLeft(6);
                            void Row(string l, string v) { t.Cell().Element(L).Text(l); t.Cell().Element(V).Text(v); }

                            Row("Fatura No", m.InvoiceNo ?? "-");
                            Row("Fatura Tarihi", m.InvoiceDate?.ToString("dd.MM.yyyy") ?? "-");
                            Row("Ödeme Yöntemi", m.PaymentMethod);
                            Row("Ödeme Tarihi", m.PaymentDate.ToString("dd.MM.yyyy"));
                            Row("Tutar", $"{m.PaymentAmount:0.##} {m.Currency}");
                            Row("Notlar", m.Notes ?? "-");
                        });

                        col.Item().PaddingTop(18).Text(t =>
                        {
                            t.Span("Onaylayan: ").SemiBold();
                            t.Span(User?.Identity?.Name ?? "-");
                        });
                    });

                    page.Footer().AlignRight().Text(t =>
                    {
                        t.Span("Sayfa ");
                        t.CurrentPageNumber();
                        t.Span(" / ");
                        t.TotalPages();
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}
