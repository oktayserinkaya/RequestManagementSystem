using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM;
using DTO.Concrete.RequestDTO; // <- UpdateRequestDTO için
using Microsoft.AspNetCore.Hosting;
using System.IO;
using RequestEntity = CORE.Entities.Concrete.Request;

namespace WEB.Areas.PaymentTransaction.Controllers
{
    [Area("PaymentTransaction")]
    [Authorize(Roles = "OdemeBirimi,Admin")]
    [Route("Finance/Payments")]
    public class PaymentTransactionController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ILogger<PaymentTransactionController> _logger;
        private readonly IWebHostEnvironment _env;

        public PaymentTransactionController(
            IRequestManager requestManager,
            ILogger<PaymentTransactionController> logger,
            IWebHostEnvironment env)
        {
            _requestManager = requestManager;
            _logger = logger;
            _env = env;
        }

        // Satın Alma'dan gelen -> ödeme bekleyenler.
        // Eğer enum'unuzda FinancePending/PaymentPending varsa:
        //   where: x => x.Status == Status.FinancePending
        // Şimdilik Modified'ı "ödeme bekliyor" gibi ele alıyoruz.
        [HttpGet("")]
        public async Task<IActionResult> Index(string? q = null, int take = 100)
        {
            var list = await _requestManager.GetFilteredListAsync(
                select: x => new PaymentTransactionListVM { /* ... */ },
                where: x =>
                    x.Status == Status.WaitingPayment &&      // 🔴 BURAYI Modified -> WaitingPayment yapın
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
        // Şartname (PDF) yeni sekmede
        [HttpGet("Spec/{id:guid}")]
        public async Task<IActionResult> Spec(Guid id)
        {
            var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(x => x.Id == id);
            var req = list.FirstOrDefault();
            if (req is null) return NotFound();

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

        // Ödeme yapıldı (kapat)
        // Not: Yeni bir enum kullanıyorsanız burada "Paid/Completed" vb. yapabilirsiniz.
        [HttpPost("Pay/{id:guid}")]    
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(Guid id)
        {
            try
            {
                var ok = await _requestManager.UpdateAsync(new UpdateRequestDTO
                {
                    Status = Status.Active,          // <- ödeme tamamlandı/kapatıldı
                    UpdatedDate = DateTime.UtcNow
                }, id);

                if (!ok)
                {
                    TempData["Error"] = "Ödeme güncellemesi başarısız.";
                }
                else
                {
                    TempData["Success"] = "Ödeme başarıyla gerçekleştirildi.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PaymentTransaction/Pay hata. Id: {Id}", id);
                TempData["Error"] = "Ödeme sırasında bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
