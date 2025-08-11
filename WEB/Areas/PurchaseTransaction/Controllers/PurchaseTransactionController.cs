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
using WEB.Areas.PurchaseTransaction.Models.PurchaseTransactionVM;
using RequestEntity = CORE.Entities.Concrete.Request;
using DTO.Concrete.RequestDTO; // <-- DTO güncellemesi için

namespace WEB.Areas.PurchaseTransaction.Controllers
{
    [Area("PurchaseTransaction")]
    [Authorize(Roles = "SatinAlmaBirimi,Admin")]
    [Route("PurchaseTransaction/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ILogger<OrdersController> _logger;
        private readonly IWebHostEnvironment _env;

        public OrdersController(IRequestManager requestManager, ILogger<OrdersController> logger, IWebHostEnvironment env)
        {
            _requestManager = requestManager;
            _logger = logger;
            _env = env;
        }

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
                        EmployeeFullName = x.Employee != null ? (x.Employee.FirstName + " " + x.Employee.LastName) : string.Empty,
                        EmployeeEmail = x.Employee != null ? (x.Employee.Email ?? string.Empty) : string.Empty,
                        DepartmentName = (x.Employee != null && x.Employee.Department != null)
                            ? (x.Employee.Department.DepartmentName ?? string.Empty)
                            : string.Empty,
                        CategoryName = (x.Product != null && x.Product.SubCategory != null && x.Product.SubCategory.Category != null)
                            ? (x.Product.SubCategory.Category.CategoryName ?? "-")
                            : "-",
                        SubCategoryName = (x.Product != null && x.Product.SubCategory != null)
                            ? (x.Product.SubCategory.SubCategoryName ?? "-")
                            : "-",
                        ProductName = (x.Product != null && !string.IsNullOrWhiteSpace(x.Product.ProductName))
                            ? x.Product.ProductName!
                            : (x.SpecialProductName ?? "-"),
                        Amount = x.Amount,
                        SpecPath = x.ProductFeaturesFilePath,
                        StatusText = ToTrStatus(x.Status)
                    },
                    // Satın alma ekranında: Komisyon onaylı olanlar
                    where: x =>
                        x.Status == Status.Modified &&
                        (string.IsNullOrWhiteSpace(q) ||
                         ((x.Employee!.FirstName + " " + x.Employee.LastName).ToLower().Contains(q!.ToLower())) ||
                         ((x.Employee.Email ?? string.Empty).ToLower().Contains(q!.ToLower())) ||
                         ((x.Employee.Department!.DepartmentName ?? string.Empty).ToLower().Contains(q!.ToLower()))),
                    orderBy: qy => qy.OrderByDescending(z => z.CreatedDate),
                    join: qy => qy
                        .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                        .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
                );

                var model = list.Take(take).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PurchaseTransaction/Orders/Index yüklenirken hata.");
                TempData["Error"] = "Kayıtlar yüklenirken bir hata oluştu.";
                return View(Enumerable.Empty<OrderListVM>());
            }
        }

        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id && x.Status == Status.Modified,
                join: q => q
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Employee!).ThenInclude(e => e.Title!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
            );

            var r = list.FirstOrDefault();
            if (r is null)
            {
                TempData["Error"] = "Talep bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Employee = r.Employee != null ? $"{r.Employee.FirstName} {r.Employee.LastName}" : "-";
            ViewBag.Department = r.Employee?.Department?.DepartmentName ?? "-";
            ViewBag.Category = r.Product?.SubCategory?.Category?.CategoryName ?? "-";
            ViewBag.SubCategory = r.Product?.SubCategory?.SubCategoryName ?? "-";
            ViewBag.Product = r.Product?.ProductName ?? (r.SpecialProductName ?? "-");
            ViewBag.Amount = r.Amount;
            ViewBag.RequestDate = r.RequestDate ?? r.CreatedDate;
            ViewBag.SpecPath = r.ProductFeaturesFilePath;

            return View();
        }

        [HttpGet("Spec/{id:guid}")]
        public async Task<IActionResult> Spec(Guid id)
        {
            var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(x => x.Id == id);
            var req = list.FirstOrDefault();
            if (req is null) return NotFound();

            var rawPath = req.ProductFeaturesFilePath;
            if (string.IsNullOrWhiteSpace(rawPath)) return NotFound("Şartname dosyası bulunamadı.");

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
            if (found == null) return NotFound("Şartname dosyası bulunamadı.");

            var fileName = Path.GetFileName(found);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileName}\"";
            return PhysicalFile(found, "application/pdf", enableRangeProcessing: true);
        }

        private static string ToTrStatus(Status status) =>
            status switch
            {
                Status.Active => "AKTİF",
                Status.Modified => "GÜNCELLENDİ",
                Status.Passive => "PASİF",
                // yeni durumlar eklediyseniz:
                Status.WaitingPayment => "ÖDEME BEKLİYOR",
                Status.Paid => "ÖDENDİ",
                _ => status.ToString().ToUpperInvariant()
            };

        // Satın alma formu
        [HttpGet("Buy/{id:guid}")]
        public async Task<IActionResult> Buy(Guid id)
        {
            var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Id == id && x.Status == Status.Modified,
                join: q => q
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
            );

            var r = list.FirstOrDefault();
            if (r is null)
            {
                TempData["Error"] = "Talep bulunamadı veya onaylı değil.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new PurchaseCreateVM
            {
                RequestId = r.Id,
                RequestDate = r.RequestDate ?? r.CreatedDate,
                EmployeeFullName = $"{r.Employee?.FirstName} {r.Employee?.LastName}".Trim(),
                DepartmentName = r.Employee?.Department?.DepartmentName ?? "-",
                CategoryName = r.Product?.SubCategory?.Category?.CategoryName ?? "-",
                SubCategoryName = r.Product?.SubCategory?.SubCategoryName ?? "-",
                ProductName = r.Product?.ProductName ?? (r.SpecialProductName ?? "-"),
                RequestedAmount = r.Amount,
                SpecPath = r.ProductFeaturesFilePath,
                Quantity = r.Amount
            };

            return View("Buy", vm);
        }

        // Satın alma formu POST -> Ödeme birimine gönder
        [HttpPost("Buy/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(Guid id, PurchaseCreateVM model)
        {
            if (model.Quantity is null || model.Quantity <= 0)
                ModelState.AddModelError(nameof(model.Quantity), "Adet giriniz.");

            if (model.UnitPrice is null || model.UnitPrice < 0)
                ModelState.AddModelError(nameof(model.UnitPrice), "Birim fiyat giriniz.");

            if (!ModelState.IsValid)
                return View("Buy", model);

            // Tutar hesapları
            var subtotal = (model.Quantity ?? 0) * (model.UnitPrice ?? 0);
            var discountAmount = subtotal * (model.DiscountRate / 100m);
            var net = subtotal - discountAmount;
            var vatAmount = net * (model.VatRate / 100m);
            var grand = net + vatAmount;

            model.Subtotal = subtotal;
            model.DiscountAmount = discountAmount;
            model.VatAmount = vatAmount;
            model.GrandTotal = grand;

            // (opsiyonel) Teklif PDF kaydı
            if (model.OfferPdf != null && model.OfferPdf.Length > 0)
            {
                var dir = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), "uploads", "purchases");
                Directory.CreateDirectory(dir);
                var fname = $"{Guid.NewGuid()}{Path.GetExtension(model.OfferPdf.FileName)}";
                var fpath = Path.Combine(dir, fname);
                using (var fs = System.IO.File.Create(fpath))
                    await model.OfferPdf.CopyToAsync(fs);
                // dosya yolunu bir yere yazmak istiyorsanız: model.OfferPdfPath = $"uploads/purchases/{fname}";
            }

            // 🔴 TALEBİ ÖDEME BİRİMİNE GÖNDER: durumu WaitingPayment yap
            var update = new UpdateRequestDTO
            {
                Status = Status.WaitingPayment,     // enum’a eklendiğini varsayıyoruz
                UpdatedDate = DateTime.UtcNow,
                // Finans’a kısa not bırakmak isterseniz mevcut bir metin alanına yazın:
                CommissionNote = $"Satınalma oluşturdu. Genel Toplam: {grand:0.##} {model.Currency}"
            };

            var ok = await _requestManager.UpdateAsync(update, id);
            if (!ok)
            {
                TempData["Error"] = "Talep güncellenemedi (Ödeme birimine gönderilemedi).";
                return View("Buy", model);
            }

            TempData["Success"] = "Satın alma emri oluşturuldu ve Ödeme Birimi’ne gönderildi.";
            // Ödeme birimi ekranına yönlendir
            return RedirectToAction("Index", "PaymentTransaction", new { area = "PaymentTransaction" });
        }
    }
}
