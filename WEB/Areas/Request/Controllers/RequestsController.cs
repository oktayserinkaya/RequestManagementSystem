using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Enums;
using CORE.Extensions;
using DTO.Concrete.CategoryDTO;
using DTO.Concrete.EmployeeDTO;
using DTO.Concrete.ProductDTO;
using DTO.Concrete.RequestDTO;
using DTO.Concrete.SubCategoryDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class RequestsController(IRequestManager requestManager, ICategoryManager categoryManager, IMapper mapper, ILogger<RequestsController> logger, IEmployeeManager employeeManager,IUserManager userManager, ISubCategoryManager subCategoryManager, IProductManager productManager) : Controller
    {
        private readonly IRequestManager _requestManager = requestManager;
        private readonly ICategoryManager _categoryManager = categoryManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<RequestsController> _logger = logger;
        private readonly IEmployeeManager _employeeManager = employeeManager;
        private readonly IUserManager _userManager = userManager;
        private readonly ISubCategoryManager _subCategoryManager = subCategoryManager;
        private readonly IProductManager _productManager = productManager;

        public async Task<IActionResult> Index()
        {
            var model = await _requestManager.GetFilteredListAsync(
                select: x => new GetRequestsVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,

                    // FullName için kaynaklar:
                    FirstName = x.Employee != null ? x.Employee.FirstName : null,
                    LastName = x.Employee != null ? x.Employee.LastName : null,
                    Email = x.Employee != null ? x.Employee.Email : null,

                    // İlişkiler:
                    DepartmentName = (x.Employee != null && x.Employee.Department != null)
                                        ? x.Employee.Department.DepartmentName
                                        : string.Empty,

                    // Title Request’teyse:
                    TitleName = (x.Title != null) ? x.Title.TitleName : string.Empty,

                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,

                    // STRING yerine ENUM'u taşı
                    StatusEnum = x.Status
                },
                where: x => x.Status != Status.Passive,
                orderBy: q => q.OrderByDescending(z => z.CreatedDate),
                join: q => q
                    .Include(z => z.Employee!).ThenInclude(z => z.Department!)
                    .Include(z => z.Title!)
            );

            // EF sorgusu bitti → bellek tarafında DisplayName'e çevir
            foreach (var item in model)
                item.Status = item.StatusEnum.GetDisplayName();

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> CreateRequest()
        {
            // Kullanıcının Id'sini al
            var userId = await _userManager.GetUserIdByClaimsAsync(User);

            // Session’da yoksa veritabanından bilgileri al
            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == userId);

            var model = new CreateRequestVM
            {
                FirstName = employee?.FirstName ?? "Personel",
                LastName = employee?.LastName ?? "Birim",
                DepartmentId = employee?.DepartmentId,
                DepartmentName = employee?.DepartmentName ?? ""
            };

            // ViewBag dropdownlarını yükle
            await SetDropdownsAsync();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                await SetDropdownsAsync();
                return View(model);
            }

            var userId = await _userManager.GetUserIdByClaimsAsync(User);

            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == userId);
            if (employee == null)
            {
                TempData["Error"] = "Personel bilgisi alınamadı.";
                return RedirectToAction("Index");
            }

            var dto = _mapper.Map<CreateRequestDTO>(model);
            var entity = _mapper.Map<CORE.Entities.Concrete.Request>(dto);

            // 👇 Ek bilgiler:
            entity.AppUserId = userId;
            entity.DepartmentId = employee.DepartmentId;

            // PDF Dosya yükleme
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
                await SetDropdownsAsync();
                return View(model);
            }

            TempData["Success"] = "Talep başarıyla oluşturuldu.";
            return RedirectToAction("Index");
        }


        private async Task SetDropdownsAsync()
        {
            var categories = await _categoryManager.GetByDefaultsAsync<CategorySelectListDTO>(x => x.Status != Status.Passive);
            var subCategories = await _subCategoryManager.GetByDefaultsAsync<SubCategorySelectListDTO>(x => x.Status != Status.Passive);
            var products = await _productManager.GetByDefaultsAsync<ProductSelectListDTO>(x => x.Status != Status.Passive);

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.SubCategories = new SelectList(subCategories, "Id", "Name");
            ViewBag.Products = new SelectList(products, "Id", "ProductName");
        }




    }
}
