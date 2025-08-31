using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.IO;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WEB.Areas.RequestEvaluation.Models.RequestEvaluationVM;
using RequestEntity = CORE.Entities.Concrete.Request;
using DTO.Concrete.RequestDTO;

namespace WEB.Areas.RequestEvaluation.Controllers
{
    [Area("RequestEvaluation")]
    [Route("RequestEvaluation")]
    [Authorize(Roles = "IhtiyacTespitKomisyonu,Admin")]
    public class RequestEvaluationController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ILogger<RequestEvaluationController> _logger;
        private readonly IWebHostEnvironment _env;

        public RequestEvaluationController(
            IRequestManager requestManager,
            ILogger<RequestEvaluationController> logger,
            IWebHostEnvironment env)
        {
            _requestManager = requestManager;
            _logger = logger;
            _env = env;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string? q = null, CORE.Enums.Status? status = null, int take = 50)
        {
            try
            {
                var requests = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                    x =>
                        (status == null || x.Status == status) &&
                        (string.IsNullOrWhiteSpace(q) ||
                         ((x.Employee!.FirstName + " " + x.Employee.LastName).ToLower().Contains(q!.ToLower())) ||
                         ((x.Employee.Email ?? string.Empty).ToLower().Contains(q!.ToLower()))),
                    join: qry => qry
                        .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                        .Include(r => r.Employee!).ThenInclude(e => e.Title!)
                );

                var model = requests
                    .OrderByDescending(r => r.CreatedDate)
                    .Take(take)
                    .Select(r => new RequestEvaluationListVM
                    {
                        Id = r.Id,
                        CreatorFullName = $"{r.Employee!.FirstName} {r.Employee.LastName}",
                        CreatorEmail = r.Employee.Email ?? string.Empty,
                        DepartmentName = r.Employee.Department?.DepartmentName ?? "-",
                        TitleName = r.Employee.Title?.TitleName ?? "-",
                        StatusText = ToTrStatus(r.Status),
                        CreatedDate = r.CreatedDate
                    })
                    .ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestEvaluation/Index yüklenirken hata.");
                TempData["Error"] = "Talepler yüklenirken bir hata oluştu.";
                return View(Enumerable.Empty<RequestEvaluationListVM>());
            }
        }

        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                    x => x.Id == id,
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

                var special = NullIfEmpty(GetString(r, "SpecialProductName", "CustomProductName", "OzelUrun"));

                var product = r.Product;
                var subCategory = product?.SubCategory;
                var category = subCategory?.Category;

                string productNameExplicit = GetEntityName(product, "ProductName", "Name", "TitleName", "Title");
                string subCatNameExplicit = GetEntityName(subCategory, "SubCategoryName", "Name", "TitleName", "Title");
                string catNameExplicit = GetEntityName(category, "CategoryName", "Name", "TitleName", "Title");

                var vm = new RequestEvaluationDetailFormVM
                {
                    Id = r.Id,
                    RequestDate = r.RequestDate,
                    CreatedDate = r.CreatedDate,

                    FullName = $"{r.Employee?.FirstName} {r.Employee?.LastName}".Trim(),
                    DepartmentName = r.Employee?.Department?.DepartmentName ?? "-",

                    CategoryName = catNameExplicit != "-" ? catNameExplicit : (special != null ? "Listeden seçilmedi" : "-"),
                    SubCategoryName = subCatNameExplicit != "-" ? subCatNameExplicit : (special != null ? "Listeden seçilmedi" : "-"),
                    ProductName = productNameExplicit != "-" ? productNameExplicit : (special != null ? "Listeden seçilmedi" : "-"),

                    SpecialProductName = special,
                    Amount = GetDecimal(r, "Amount", "Quantity", "Adet", "Miktar"),
                    Description = NullIfEmpty(GetString(r, "Description", "Explanation", "Aciklama")),
                    ExistingProductFeaturesFilePath = NullIfEmpty(GetString(r, "ProductFeaturesFilePath", "SpecFilePath", "SartnamePath")),
                    StatusText = ToTrStatus(r.Status)
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestEvaluation/Detail hata. Id: {Id}", id);
                TempData["Error"] = "Talep detayı gösterilirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        // === ONAY ===
        [HttpPost("Approve")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "IhtiyacTespitKomisyonu,Admin")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var dto = new UpdateRequestDTO
            {
                Status = CORE.Enums.Status.Modified,   // Satın Alma’ya aktarılmış/Onaylanmış
                UpdatedDate = DateTime.UtcNow
            };

            var ok = await _requestManager.UpdateAsync(dto, id);
            if (!ok)
            {
                TempData["Error"] = "Talep onaylanırken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Talep onaylandı ve Satın Alma birimine yönlendirildi.";
            
            return RedirectToAction("Index", "Orders", new { area = "PurchaseTransaction" });
        }

        // === RED ===
        [HttpPost("Reject")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "IhtiyacTespitKomisyonu,Admin")]
        public async Task<IActionResult> Reject(Guid id, string? reason)
        {
            var dto = new UpdateRequestDTO
            {
                Status = CORE.Enums.Status.Passive,
                UpdatedDate = DateTime.UtcNow
            };

            TrySetStringOnObject(dto, new[] { "CommissionNote", "RejectionReason", "RejectReason", "KomisyonNotu" }, reason);

            var ok = await _requestManager.UpdateAsync(dto, id);
            TempData[ok ? "Success" : "Error"] = ok ? "Talep reddedildi." : "Talep reddedilirken bir hata oluştu.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Spec/{id:guid}")]
        public async Task<IActionResult> Spec(Guid id)
        {
            var list = await _requestManager.GetByDefaultsAsync<RequestEntity>(x => x.Id == id);
            var req = list.FirstOrDefault();
            if (req is null) return NotFound();

            var rawPath = GetString(req, "ProductFeaturesFilePath", "SpecFilePath", "SartnamePath");
            if (string.IsNullOrWhiteSpace(rawPath) || rawPath == "-")
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

        // --- helpers ---

        private static void TrySetStringOnObject(object obj, string[] names, string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            foreach (var n in names)
            {
                var p = obj.GetType().GetProperty(n,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (p != null && p.PropertyType == typeof(string) && p.CanWrite)
                {
                    p.SetValue(obj, value);
                    break;
                }
            }
        }

        private static string GetEntityName(object? entity, params string[] candidates)
        {
            if (entity == null) return "-";
            foreach (var n in candidates)
            {
                var p = entity.GetType().GetProperty(n,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (p == null || p.PropertyType != typeof(string)) continue;
                var val = p.GetValue(entity) as string;
                if (!string.IsNullOrWhiteSpace(val)) return val!;
            }
            return "-";
        }

        private static string ToTrStatus(CORE.Enums.Status status) =>
            status switch
            {
                CORE.Enums.Status.Active => "AKTİF",
                CORE.Enums.Status.Modified => "GÜNCELLENDİ",
                CORE.Enums.Status.Passive => "PASİF",
                _ => status.ToString().ToUpperInvariant()
            };

        private static string GetString(object obj, params string[] names)
        {
            foreach (var n in names)
            {
                var prop = obj.GetType().GetProperty(n, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null) continue;
                if (prop.PropertyType != typeof(string)) continue;
                var val = prop.GetValue(obj) as string;
                if (!string.IsNullOrWhiteSpace(val)) return val!;
            }
            return "-";
        }

        private static decimal? GetDecimal(object obj, params string[] names)
        {
            foreach (var n in names)
            {
                var prop = obj.GetType().GetProperty(n, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (prop == null) continue;
                var val = prop.GetValue(obj);
                if (val == null) continue;
                try { return Convert.ToDecimal(val); } catch { }
            }
            return null;
        }

        private static string? NullIfEmpty(string s) =>
            string.IsNullOrWhiteSpace(s) || s == "-" ? null : s;

        private static string FindNestedString(object obj, string[] preferred, string[] containerHints, int maxDepth = 3)
        {
            var direct = GetString(obj, preferred);
            if (direct != "-") return direct;

            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            return FindRecursive(obj, 0) ?? "-";

            string? FindRecursive(object current, int depth)
            {
                if (current == null || depth > maxDepth) return null;
                if (!visited.Add(current)) return null;

                var type = current.GetType();
                foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (p.GetIndexParameters().Length > 0) continue;
                    var pt = p.PropertyType;
                    if (pt == typeof(string) || pt.IsPrimitive) continue;

                    if (typeof(IEnumerable).IsAssignableFrom(pt) && pt != typeof(string))
                    {
                        if (p.GetValue(current) is IEnumerable coll)
                        {
                            var en = coll.GetEnumerator();
                            if (en.MoveNext())
                            {
                                var first = en.Current;
                                var found = FindRecursive(first!, depth + 1);
                                if (!string.IsNullOrWhiteSpace(found)) return found;
                            }
                        }
                        continue;
                    }

                    var child = p.GetValue(current);
                    if (child == null) continue;

                    var s = GetString(child, preferred);
                    if (s != "-") return s;

                    var nameHit =
                        containerHints.Any(h => p.Name.IndexOf(h, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        containerHints.Any(h => pt.Name.IndexOf(h, StringComparison.OrdinalIgnoreCase) >= 0);

                    if (nameHit)
                    {
                        var deeper = FindRecursive(child, depth + 1);
                        if (!string.IsNullOrWhiteSpace(deeper)) return deeper;
                    }
                }
                return null;
            }
        }

        sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public static readonly ReferenceEqualityComparer Instance = new();
            public new bool Equals(object x, object y) => ReferenceEquals(x, y);
            public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
        }
    }
}
