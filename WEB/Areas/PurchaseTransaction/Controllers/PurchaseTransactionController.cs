using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RequestEntity = CORE.Entities.Concrete.Request;

using DTO.Concrete.RequestDTO;    // UpdateRequestDTO
using DTO.Concrete.PurchaseDTO;   // CreateOrUpdatePurchaseDTO
using WEB.Areas.PurchaseTransaction.Models.PurchaseTransactionVM; // OrderListVM

namespace WEB.Areas.PurchaseTransaction.Controllers
{
    [Area("PurchaseTransaction")]
    [Authorize(Roles = "SatinAlmaBirimi,Admin")]
    [Route("PurchaseTransaction/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly IPurchaseManager _purchaseManager;
        private readonly ILogger<OrdersController> _logger;
        private readonly IWebHostEnvironment _env;

        public OrdersController(
            IRequestManager requestManager,
            IPurchaseManager purchaseManager,
            ILogger<OrdersController> logger,
            IWebHostEnvironment env)
        {
            _requestManager = requestManager;
            _purchaseManager = purchaseManager;
            _logger = logger;
            _env = env;
        }

        // --------------------------------------------------------------------
        // LISTE
        // --------------------------------------------------------------------
        [HttpGet("")]
        public async Task<IActionResult> Index(string? q = null, int take = 100)
        {
            try
            {
                var list = await _requestManager.GetFilteredListAsync(
                    select: x => new OrderListVM
                    {
                        Id = x.Id,
                        RequestDate = x.RequestDate,
                        CreatedDate = x.CreatedDate,

                        EmployeeFullName = (x.Employee != null
                            ? (x.Employee.FirstName + " " + x.Employee.LastName)
                            : string.Empty),

                        EmployeeEmail = (x.Employee != null && x.Employee.Email != null
                            ? x.Employee.Email
                            : string.Empty),

                        DepartmentName = (x.Employee != null && x.Employee.Department != null
                            ? x.Employee.Department.DepartmentName
                            : "-"),

                        CategoryName = (x.Product != null &&
                                        x.Product.SubCategory != null &&
                                        x.Product.SubCategory.Category != null
                            ? x.Product.SubCategory.Category.CategoryName
                            : "-"),

                        SubCategoryName = (x.Product != null &&
                                           x.Product.SubCategory != null
                            ? x.Product.SubCategory.SubCategoryName
                            : "-"),

                        ProductName = (x.Product != null
                            ? x.Product.ProductName
                            : (x.SpecialProductName != null ? x.SpecialProductName : "-")),

                        // Eğer OrderListVM.Amount decimal? ise:
                        Amount = x.Amount == null ? (decimal?)null : (decimal?)x.Amount,

                        SpecPath = x.ProductFeaturesFilePath,
                        StatusText = ToTrStatus(x.Status)
                    },

                    where: x =>
                        x.Status == Status.Modified &&
                        (string.IsNullOrWhiteSpace(q) ||
                         ((x.Employee != null
                             ? (x.Employee.FirstName + " " + x.Employee.LastName)
                             : string.Empty).ToLower().Contains(q!.ToLower())) ||
                         ((x.Employee != null && x.Employee.Email != null
                             ? x.Employee.Email
                             : string.Empty).ToLower().Contains(q!.ToLower())) ||
                         ((x.Employee != null && x.Employee.Department != null
                             ? x.Employee.Department.DepartmentName
                             : string.Empty).ToLower().Contains(q!.ToLower()))),

                    orderBy: qy => qy.OrderByDescending(z => z.CreatedDate),

                    join: qy => qy
                        .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                        .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
                );

                return View(list.Take(take).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PurchaseTransaction/Orders/Index yüklenirken hata.");
                TempData["Error"] = "Kayıtlar yüklenirken bir hata oluştu.";
                return View(Enumerable.Empty<OrderListVM>());
            }
        }

        // --------------------------------------------------------------------
        // DETAY
        // --------------------------------------------------------------------
        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var r = (await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id && x.Status == Status.Modified,
                join: q => q
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Employee!).ThenInclude(e => e.Title!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
            )).FirstOrDefault();

            if (r == null)
            {
                TempData["Error"] = "Talep bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Employee = r.Employee != null ? $"{r.Employee.FirstName} {r.Employee.LastName}".Trim() : "-";
            ViewBag.Department = (r.Employee != null && r.Employee.Department != null) ? r.Employee.Department.DepartmentName : "-";
            ViewBag.Category = (r.Product != null && r.Product.SubCategory != null && r.Product.SubCategory.Category != null)
                ? r.Product.SubCategory.Category.CategoryName
                : "-";
            ViewBag.SubCategory = (r.Product != null && r.Product.SubCategory != null)
                ? r.Product.SubCategory.SubCategoryName
                : "-";
            ViewBag.Product = (r.Product != null) ? r.Product.ProductName : (r.SpecialProductName ?? "-");

          
            ViewBag.Amount = r.Amount == null ? (decimal?)null : (decimal?)r.Amount;

            ViewBag.RequestDate = r.RequestDate ?? r.CreatedDate;
            ViewBag.SpecPath = r.ProductFeaturesFilePath;

            return View();
        }

        // --------------------------------------------------------------------
        // ŞARTNAME GÖRÜNTÜLE
        // --------------------------------------------------------------------
        [HttpGet("Spec/{id:guid}")]
        public async Task<IActionResult> Spec(Guid id)
        {
            var req = (await _requestManager.GetByDefaultsAsync<RequestEntity>(x => x.Id == id)).FirstOrDefault();
            if (req == null) return NotFound();

            var rawPath = req.ProductFeaturesFilePath;
            if (string.IsNullOrWhiteSpace(rawPath))
                return NotFound("Şartname dosyası bulunamadı.");

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
                return NotFound("Şartname dosyası bulunamadı.");

            var fileName = Path.GetFileName(found);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileName}\"";
            return PhysicalFile(found, "application/pdf", enableRangeProcessing: true);
        }

        // --------------------------------------------------------------------
        // SATIN ALMA FORMU (GET)
        // --------------------------------------------------------------------
        // --- GET: Buy ---------------------------------------------------------------
        [HttpGet("Buy/{id:guid}")]
        public async Task<IActionResult> Buy(Guid id)
        {
            var r = (await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id && x.Status == Status.Modified,
                join: q => q
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
                    .Include(r => r.Purchase!)
            )).FirstOrDefault();

            if (r == null)
            {
                TempData["Error"] = "Talep bulunamadı veya onaylı değil.";
                return RedirectToAction(nameof(Index));
            }

            var p = r.Purchase;

            var vm = new DTO.Concrete.PurchaseDTO.PurchaseCreateVM
            {
                RequestId = r.Id,
                RequestDate = r.RequestDate ?? r.CreatedDate,
                EmployeeFullName = $"{r.Employee?.FirstName} {r.Employee?.LastName}".Trim(),
                DepartmentName = (r.Employee != null && r.Employee.Department != null) ? r.Employee.Department.DepartmentName : "-",
                CategoryName = (r.Product != null && r.Product.SubCategory != null && r.Product.SubCategory.Category != null)
                                    ? r.Product.SubCategory.Category.CategoryName : "-",
                SubCategoryName = (r.Product != null && r.Product.SubCategory != null)
                                    ? r.Product.SubCategory.SubCategoryName : "-",
                ProductName = r.Product?.ProductName,
                SpecialProductName = r.SpecialProductName, 
                RequestedAmount = r.Amount == null ? (decimal?)null : (decimal?)r.Amount,
                SpecPath = r.ProductFeaturesFilePath,

                // p.Quantity: int?  -> VM.Quantity: decimal?
                Quantity = p?.Quantity.HasValue == true
                                    ? (decimal?)p.Quantity.Value
                                    : (r.Amount == null ? (decimal?)null : (decimal?)r.Amount),

                UnitPrice = p?.UnitPrice,
                DiscountRate = p?.DiscountRate ?? 0,
                VatRate = p?.VatRate ?? 20,
                Subtotal = p?.Subtotal,
                DiscountAmount = p?.DiscountAmount,
                VatAmount = p?.VatAmount,
                GrandTotal = p?.GrandTotal,
                Currency = p?.Currency ?? "TRY",
                OfferPdfPath = p?.OfferPdfPath,

                OfferNo = p?.OfferNo,
                OfferDate = p?.OfferDate,
                PaymentTerms = p?.PaymentTerms,
                Notes = p?.Notes,
                DeliveryDate = p?.DeliveryDate
            };

            return View("Buy", vm);
        }

        // --- POST: Buy --------------------------------------------------------------
        [HttpPost("Buy/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(Guid id, DTO.Concrete.PurchaseDTO.PurchaseCreateVM model)
        {
            // Validasyon
            if (model.Quantity is null || model.Quantity <= 0)
                ModelState.AddModelError(nameof(model.Quantity), "Adet giriniz.");

            if (model.UnitPrice is null || model.UnitPrice < 0)
                ModelState.AddModelError(nameof(model.UnitPrice), "Birim fiyat giriniz.");

            if (!ModelState.IsValid)
                return View("Buy", model);

            // Hesaplamalar
            var qty = model.Quantity ?? 0m;
            var unit = model.UnitPrice ?? 0m;
            var discRate = model.DiscountRate < 0 ? 0 : model.DiscountRate;
            var vatRate = model.VatRate < 0 ? 0 : model.VatRate;

            var subtotal = qty * unit;
            var discountAmount = subtotal * (discRate / 100m);
            var net = subtotal - discountAmount;
            var vatAmount = net * (vatRate / 100m);
            var grand = net + vatAmount;

            // Ekrana geri yaz
            model.Subtotal = subtotal;
            model.DiscountAmount = discountAmount;
            model.VatAmount = vatAmount;
            model.GrandTotal = grand;

            // Teklif PDF kaydı
            string? savedOfferPath = null;
            if (model.OfferPdf != null && model.OfferPdf.Length > 0)
            {
                var root = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
                var dir = Path.Combine(root, "uploads", "purchases");
                Directory.CreateDirectory(dir);

                var fname = $"{Guid.NewGuid()}{Path.GetExtension(model.OfferPdf.FileName)}";
                var fpath = Path.Combine(dir, fname);
                using (var fs = System.IO.File.Create(fpath))
                    await model.OfferPdf.CopyToAsync(fs);

                savedOfferPath = $"uploads/purchases/{fname}";
            }

            var purchaseDto = new CreateOrUpdatePurchaseDTO
            {
                RequestId = id,

                SupplierName = model.SupplierName,
                SupplierTaxNo = model.SupplierTaxNo,
                SupplierIban = model.SupplierIban,
                SupplierEmail = model.SupplierEmail,
                SupplierPhone = model.SupplierPhone,

                OfferNo = model.OfferNo,
                OfferDate = model.OfferDate,
                PaymentTerms = model.PaymentTerms,
                Notes = model.Notes,
                DeliveryDate = model.DeliveryDate,

                Quantity = model.Quantity.HasValue
                                   ? (int?)Convert.ToInt32(Math.Round(model.Quantity.Value, 0, MidpointRounding.AwayFromZero))
                                   : (int?)null,

                UnitPrice = model.UnitPrice,
                DiscountRate = discRate,
                VatRate = vatRate,
                Subtotal = subtotal,
                DiscountAmount = discountAmount,
                VatAmount = vatAmount,
                GrandTotal = grand,
                Currency = model.Currency,

                OfferPdfPath = savedOfferPath ?? model.OfferPdfPath,

                
                SpecialProductName = model.SpecialProductName?.Trim()
            };

            var saved = await _purchaseManager.UpsertAsync(purchaseDto);
            if (!saved)
            {
                TempData["Error"] = "Satın alma bilgileri kaydedilemedi.";
                return View("Buy", model);
            }

            var update = new UpdateRequestDTO
            {
                Status = Status.WaitingPayment,
                UpdatedDate = DateTime.UtcNow,
                CommissionNote = $"Satınalma oluşturdu. Genel Toplam: {grand:0.##} {model.Currency}"
            };

            var ok = await _requestManager.UpdateAsync(update, id);
            if (!ok)
            {
                TempData["Error"] = "Talep güncellenemedi (Ödeme birimine gönderilemedi).";
                return View("Buy", model);
            }

            TempData["Success"] = "Satın alma emri oluşturuldu ve Ödeme Birimi’ne gönderildi.";
            return RedirectToAction("Index", "PaymentTransaction", new { area = "PaymentTransaction" });
        }




        // --------------------------------------------------------------------
        // HELPER
        // --------------------------------------------------------------------
        private static string ToTrStatus(Status status) =>
            status switch
            {
                Status.Active => "AKTİF",
                Status.Modified => "GÜNCELLENDİ",
                Status.Passive => "PASİF",
                Status.WaitingPayment => "ÖDEME BEKLİYOR",
                Status.Paid => "ÖDENDİ",
                _ => status.ToString().ToUpperInvariant()
            };
    }
}
