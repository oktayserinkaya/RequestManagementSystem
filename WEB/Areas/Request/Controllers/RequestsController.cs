using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using CORE.Extensions;
using DTO.Concrete.CategoryDTO;
using DTO.Concrete.EmployeeDTO;
using DTO.Concrete.ProductDTO;
using DTO.Concrete.RequestDTO;
using DTO.Concrete.SubCategoryDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;
using RequestEntity = CORE.Entities.Concrete.Request;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    [Authorize]
    [Route("[area]/[controller]")]
    public class RequestsController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ICategoryManager _categoryManager;
        private readonly ISubCategoryManager _subCategoryManager;
        private readonly IProductManager _productManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestsController> _logger;

        public RequestsController(
            IRequestManager requestManager,
            ICategoryManager categoryManager,
            IMapper mapper,
            ILogger<RequestsController> logger,
            IEmployeeManager employeeManager,
            IUserManager userManager,
            ISubCategoryManager subCategoryManager,
            IProductManager productManager)
        {
            _requestManager = requestManager;
            _categoryManager = categoryManager;
            _mapper = mapper;
            _logger = logger;
            _employeeManager = employeeManager;
            _userManager = userManager;
            _subCategoryManager = subCategoryManager;
            _productManager = productManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var model = await _requestManager.GetFilteredListAsync(
                select: x => new GetRequestsVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,
                    FirstName = x.Employee != null ? x.Employee.FirstName : null,
                    LastName = x.Employee != null ? x.Employee.LastName : null,
                    Email = x.Employee != null ? x.Employee.Email : null,
                    DepartmentName = (x.Employee != null && x.Employee.Department != null)
                                       ? x.Employee.Department.DepartmentName
                                       : string.Empty,
                    TitleName = (x.Title != null) ? x.Title.TitleName : string.Empty,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    StatusEnum = x.Status
                },
                where: x => x.Status != Status.Passive,
                orderBy: q => q.OrderByDescending(z => z.CreatedDate),
                join: q => q
                    .Include(z => z.Employee!).ThenInclude(z => z.Department!)
                    .Include(z => z.Title!)
            );

            foreach (var item in model)
                item.Status = item.StatusEnum.GetDisplayName();

            return View(model);
        }

        // GET: /Request/Requests/CreateRequest
        [HttpGet("CreateRequest")]
        public async Task<IActionResult> CreateRequest()
        {
            var vm = new CreateRequestVM
            {
                RequestDate = DateTime.Today
            };

            // Talep eden kişi/departman alanlarını doldur
            await PopulateRequesterFieldsAsync(vm);

            await FillDropdownsAsync(vm);
            return View(vm);
        }

        [HttpPost("CreateRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateRequestVM model)
        {
            await PopulateRequesterFieldsAsync(model);

            if (!model.RequestDate.HasValue)
                model.RequestDate = DateTime.Today;

            if (!model.ProductId.HasValue && string.IsNullOrWhiteSpace(model.SpecialProductName))
                ModelState.AddModelError(nameof(model.ProductId), "Ürün seçin veya Özel Ürün Adı girin.");

            if (model.ProductFeaturesFile != null)
            {
                var file = model.ProductFeaturesFile;
                var isPdf = file.ContentType?.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) == true
                            || Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase);
                if (!isPdf)
                    ModelState.AddModelError(nameof(model.ProductFeaturesFile), "Sadece PDF yükleyebilirsiniz.");
                const long maxBytes = 10 * 1024 * 1024;
                if (file.Length > maxBytes)
                    ModelState.AddModelError(nameof(model.ProductFeaturesFile), "Dosya boyutu 10MB'ı geçemez.");
            }

            if (!ModelState.IsValid)
            {
                await FillDropdownsAsync(model);
                return View(model);
            }

            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var employee = await _employeeManager.GetWithDepartmentByAppUserIdAsync(userId); // Include'lu versiyon

            if (employee == null)
            {
                TempData["Error"] = "Personel bilgisi alınamadı.";
                await FillDropdownsAsync(model);
                return View(model);
            }

            if (!employee.DepartmentId.HasValue)
            {
                ModelState.AddModelError("", "Personelin departmanı tanımlı değil.");
                await FillDropdownsAsync(model);
                return View(model);
            }

            // Eğer Request.TitleId zorunlu bir alan ise:
            if (!employee.TitleId.HasValue)
            {
                ModelState.AddModelError("", "Personelin unvanı (Title) tanımlı değil.");
                await FillDropdownsAsync(model);
                return View(model);
            }

            // VM -> DTO -> Entity
            var dto = _mapper.Map<CreateRequestDTO>(model);
            var entity = _mapper.Map<CORE.Entities.Concrete.Request>(dto);

            // 🔴 ZORUNLU FK’LERİ AYARLA
            entity.AppUserId = userId;
            entity.EmployeeId = employee.Id;                 // <- ÖNEMLİ
            entity.DepartmentId = employee.DepartmentId.Value; // <- ÖNEMLİ
            entity.TitleId = employee.TitleId.Value;      // <- Title zorunluysa

            if (model.ProductId.HasValue)
            {
                entity.ProductId = model.ProductId.Value;
            }
            else
            {
                ModelState.AddModelError(nameof(model.ProductId), "Lütfen bir ürün seçiniz.");
                await FillDropdownsAsync(model);
                return View(model);
            }

            // Dosya yükleme
            if (dto.ProductFeaturesFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProductFeaturesFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ProductFeaturesFile.CopyToAsync(stream);
                entity.ProductFeaturesFilePath = fileName;
            }

            var result = await _requestManager.AddEntityAsync(entity);
            if (!result)
            {
                TempData["Error"] = "Talep oluşturulamadı.";
                await FillDropdownsAsync(model);
                return View(model);
            }

            TempData["Success"] = "Talep başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }   


        [HttpGet("UpdateRequest/{id:guid}")]
        public async Task<IActionResult> UpdateRequest(Guid id)
        {
            var entity = await _requestManager.GetByDefaultAsync<RequestEntity>(
                x => x.Id == id && x.Status != CORE.Enums.Status.Passive,
                join: q => q
                    .Include(z => z.Employee)!.ThenInclude(z => z!.Department!)
                    .Include(z => z.Title!)
                    .Include(z => z.Product)!.ThenInclude(p => p!.SubCategory)!.ThenInclude(sc => sc!.Category)!
            );

            if (entity == null)
            {
                TempData["Error"] = "Talep bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new UpdateRequestVM
            {
                Id = entity.Id,
                RequestDate = entity.RequestDate ?? DateTime.Today,
                SpecialProductName = entity.SpecialProductName,
                Amount = entity.Amount,
                Description = entity.CommissionNote,
                ProductId = entity.ProductId,
                ProductFeaturesFilePath = entity.ProductFeaturesFilePath,
                AppUserId = entity.AppUserId,
                EmployeeId = entity.EmployeeId,
                DepartmentId = entity.DepartmentId,
                TitleId = entity.TitleId,

                // seçili hiyerarşi
                SubCategoryId = entity.Product?.SubCategoryId,
                CategoryId = entity.Product?.SubCategory?.CategoryId
            };

            await FillDropdownsAsync(vm);
            return View("UpdateRequest", vm);

        }

        [HttpPost("UpdateRequest/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRequest(Guid id, UpdateRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                await FillDropdownsAsync(model);
                return View("UpdateRequest", model);
            }

            var entity = await _requestManager.GetByDefaultAsync<RequestEntity>(
                x => x.Id == id && x.Status != CORE.Enums.Status.Passive
            );

            if (entity == null)
            {
                TempData["Error"] = "Güncellenecek talep bulunamadı.";
                await FillDropdownsAsync(model);
                return View("UpdateRequest", model);
            }

            // Alanlar
            entity.RequestDate = model.RequestDate;
            entity.SpecialProductName = model.SpecialProductName;
            entity.Amount = model.Amount;
            entity.CommissionNote = model.Description;
            entity.ProductId = model.ProductId!.Value;
            entity.UpdatedDate = DateTime.Now;
            entity.Status = CORE.Enums.Status.Modified;

            // Şartname yükleme
            if (model.ProductFeaturesFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProductFeaturesFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using var stream = new FileStream(filePath, FileMode.Create);
                await model.ProductFeaturesFile.CopyToAsync(stream);
                entity.ProductFeaturesFilePath = fileName;
            }

            var ok = await _requestManager.UpdateEntityAsync(entity);
            if (!ok)
            {
                TempData["Error"] = "Talep güncellenemedi.";
                await FillDropdownsAsync(model);
                return View("UpdateRequest", model);
            }

            TempData["Success"] = "Talep başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            // DTO yerine entity çekiyoruz -> mapping hatası olmaz
            var entity = await _requestManager.GetByDefaultAsync<RequestEntity>(
                x => x.Id == id && x.Status != CORE.Enums.Status.Passive
            );

            if (entity is null)
            {
                TempData["Error"] = "Talep bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var ok = await _requestManager.DeleteAsync(id); // BaseService.DeleteAsync: Status = Passive yapar
            if (!ok)
            {
                TempData["Error"] = "Talep silinemedi!";
                return RedirectToAction(nameof(Index));
            }

            // Şartname dosyası var ise fiziksel dosyayı da sil
            if (!string.IsNullOrEmpty(entity.ProductFeaturesFilePath))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", entity.ProductFeaturesFilePath);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            TempData["Success"] = "Talep başarıyla silindi!";
            return RedirectToAction(nameof(Index));
        }

        // --- Helpers ---

        /// <summary>
        /// Giriş yapan kullanıcının ad/soyad ve departman bilgilerini Employee üzerinden doldurur.
        /// Employee bulunamazsa claims fallback’i dener.
        /// </summary>
        private async Task PopulateRequesterFieldsAsync(CreateRequestVM vm)
        {
            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var employee = await _employeeManager.GetWithDepartmentByAppUserIdAsync(userId);

            if (employee != null)
            {
                vm.FirstName = employee.FirstName;
                vm.LastName = employee.LastName;
                vm.DepartmentId = employee.DepartmentId;
                vm.DepartmentName = employee.DepartmentName;
                return;
            }

            // Fallback: claims
            vm.FirstName = vm.FirstName ?? User.FindFirst("given_name")?.Value
                                        ?? User.FindFirst("FirstName")?.Value;
            vm.LastName = vm.LastName ?? User.FindFirst("family_name")?.Value
                                      ?? User.FindFirst("LastName")?.Value;
            vm.DepartmentName = vm.DepartmentName ?? User.FindFirst("Department")?.Value;
        }


        private async Task FillDropdownsAsync(CreateRequestVM model)
        {
            var categories = await _categoryManager
                .GetByDefaultsAsync<CategorySelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<CategorySelectListDTO>();

            var subCategories = await _subCategoryManager
                .GetByDefaultsAsync<SubCategorySelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<SubCategorySelectListDTO>();

            var products = await _productManager
                .GetByDefaultsAsync<ProductSelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<ProductSelectListDTO>();

            model.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);
            model.SubCategoryList = new SelectList(subCategories, "Id", "Name", model.SubCategoryId);
            model.ProductList = new SelectList(products, "Id", "Name", model.ProductId);
        }

        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories([FromQuery] Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                return Json(Array.Empty<SubCategorySelectListDTO>());

            var subCategories = await _subCategoryManager
                .GetByDefaultsAsync<SubCategorySelectListDTO>(x => x.Status != Status.Passive && x.CategoryId == categoryId)
                ?? new List<SubCategorySelectListDTO>();

            var result = subCategories.Select(s => new { s.Id, s.Name }).ToList();
            return Json(result);
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] Guid subCategoryId)
        {
            if (subCategoryId == Guid.Empty)
                return Json(Array.Empty<ProductSelectListDTO>());

            var products = await _productManager
                .GetByDefaultsAsync<ProductSelectListDTO>(x => x.Status != Status.Passive && x.SubCategoryId == subCategoryId)
                ?? new List<ProductSelectListDTO>();

            var result = products.Select(p => new { p.Id, p.Name }).ToList();
            return Json(result);
        }

        private async Task PopulateRequesterFieldsAsync(UpdateRequestVM vm)
        {
            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var employee = await _employeeManager.GetWithDepartmentByAppUserIdAsync(userId);

            if (employee != null)
            {
                vm.FirstName = employee.FirstName;
                vm.LastName = employee.LastName;
                vm.DepartmentId = employee.DepartmentId;
                vm.DepartmentName = employee.DepartmentName;
                return;
            }

            // Fallback: Claims
            vm.FirstName = vm.FirstName ?? User.FindFirst("given_name")?.Value
                                        ?? User.FindFirst("FirstName")?.Value;
            vm.LastName = vm.LastName ?? User.FindFirst("family_name")?.Value
                                        ?? User.FindFirst("LastName")?.Value;
            vm.DepartmentName = vm.DepartmentName ?? User.FindFirst("Department")?.Value;
        }

        private async Task FillDropdownsAsync(UpdateRequestVM model)
        {
            var categories = await _categoryManager
                .GetByDefaultsAsync<CategorySelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<CategorySelectListDTO>();

            var subCategories = await _subCategoryManager
                .GetByDefaultsAsync<SubCategorySelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<SubCategorySelectListDTO>();

            var products = await _productManager
                .GetByDefaultsAsync<ProductSelectListDTO>(x => x.Status != Status.Passive)
                ?? new List<ProductSelectListDTO>();

            model.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);
            model.SubCategoryList = new SelectList(subCategories, "Id", "Name", model.SubCategoryId);
            model.ProductList = new SelectList(products, "Id", "Name", model.ProductId);
        }




    }
}
