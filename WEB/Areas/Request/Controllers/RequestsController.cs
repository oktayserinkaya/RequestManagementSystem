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

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    [Authorize] // tüm aksiyonlar için yetki
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _requestManager.GetFilteredListAsync(
                select: x => new GetRequestsVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,

                    // FullName kaynakları
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

            // Enum -> DisplayName
            foreach (var item in model)
                item.Status = item.StatusEnum.GetDisplayName();

            return View(model);
        }

        // GET: /Request/Requests/CreateRequest
        [HttpGet]
        public async Task<IActionResult> CreateRequest()
        {
            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == userId);

            if (employee == null)
            {
                TempData["Error"] = "Personel bilgisi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new CreateRequestVM
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.DepartmentName,
                RequestDate = DateTime.Today
            };

            await FillDropdownsAsync(model);
            return View(model); // Areas/Request/Views/Requests/CreateRequest.cshtml
        }

        // POST: /Request/Requests/CreateRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                await FillDropdownsAsync(model);
                return View(model);
            }

            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == userId);
            if (employee == null)
            {
                TempData["Error"] = "Personel bilgisi alınamadı.";
                return RedirectToAction(nameof(Index));
            }

            // VM -> DTO -> Entity
            var dto = _mapper.Map<CreateRequestDTO>(model);
            var entity = _mapper.Map<CORE.Entities.Concrete.Request>(dto);

            // Ek zorunlu alanlar
            entity.AppUserId = userId;
            entity.DepartmentId = employee.DepartmentId;

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

        /// <summary>
        /// Dropdown’ları ViewModel üzerinden doldurur (ViewBag yok).
        /// Boş koleksiyonlar gönderilir; null bırakılmaz.
        /// </summary>
        private async Task FillDropdownsAsync(CreateRequestVM model)
        {
            var categories = await _categoryManager.GetByDefaultsAsync<CategorySelectListDTO>(x => x.Status != Status.Passive)
                                ?? new List<CategorySelectListDTO>();
            var subCategories = await _subCategoryManager.GetByDefaultsAsync<SubCategorySelectListDTO>(x => x.Status != Status.Passive)
                                ?? new List<SubCategorySelectListDTO>();
            var products = await _productManager.GetByDefaultsAsync<ProductSelectListDTO>(x => x.Status != Status.Passive)
                                ?? new List<ProductSelectListDTO>();

            // DİKKAT: DTO’larda "Name" property’si dolu olmalı (mappingte ProductName->Name vb.)
            model.CategoryList = new SelectList(categories, "Id", "Name");
            model.SubCategoryList = new SelectList(subCategories, "Id", "Name");
            model.ProductList = new SelectList(products, "Id", "Name");
        }
    }
}
