using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Enums;
using CORE.Extensions;
using DTO.Concrete.EmployeeDTO;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class RequestsController(IRequestManager requestManager, ICategoryManager categoryManager, IMapper mapper, ILogger<RequestsController> logger, IEmployeeManager employeeManager,IUserManager userManager) : Controller
    {
        private readonly IRequestManager _requestManager = requestManager;
        private readonly ICategoryManager _categoryManager = categoryManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<RequestsController> _logger = logger;
        private readonly IEmployeeManager _employeeManager = employeeManager;
        private readonly IUserManager _userManager = userManager;

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


        [HttpGet]
        public async Task<IActionResult> CreateRequest()
        {
            // Session’dan kullanıcı bilgilerini al
            var firstName = HttpContext.Session.GetString("FirstName");
            var lastName = HttpContext.Session.GetString("LastName");
            var department = HttpContext.Session.GetString("Department");
                     

            await SetDropdownsAsync(); // Dropdown'ları hazırla

            var vm = new CreateRequestVM
            {
                RequestDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                DepartmentName = department,
                CategoryName = string.Empty,
                SubCategoryName = string.Empty,
                ProductName = string.Empty
            };

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState geçersiz. Hatalar:");

                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogWarning("[ModelState] Alan: {Key} - Hata: {Error}", entry.Key, error.ErrorMessage);
                    }
                }

                await SetDropdownsAsync();
                return View(model);
            }

            // 👤 Giriş yapan kullanıcının Id'si
            var userId = await _userManager.GetUserIdByClaimsAsync(User);

            // 👔 Kullanıcıya karşılık gelen Employee kaydı
            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == userId);
            if (employee == null)
            {
                _logger.LogWarning("Talep oluşturan kullanıcıya ait çalışan bilgisi bulunamadı.");
                TempData["Error"] = "Kullanıcı bilgisi eksik. Lütfen sistem yöneticisine başvurun.";
                await SetDropdownsAsync();
                return View(model);
            }

            // DTO to Entity mapping
            var dto = _mapper.Map<CreateRequestDTO>(model);
            var entity = _mapper.Map<CORE.Entities.Concrete.Request>(dto);

            // 🎯 Employee bilgileri ekleniyor
            entity.EmployeeId = employee.Id;
            entity.DepartmentId = employee.DepartmentId;

            // 📎 Dosya yükleme işlemi
            if (dto.ProductFeaturesFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProductFeaturesFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ProductFeaturesFile.CopyToAsync(stream);

                entity.ProductFeaturesFilePath = fileName;
            }

            // 🪵 Loglama
            _logger.LogInformation("Kayıt öncesi kontrol -> Id: {Id}, Status: {Status}", entity.Id, entity.Status);

            // Kaydet
            var result = await _requestManager.AddEntityAsync(entity);

            if (!result)
            {
                _logger.LogWarning("Request kaydı başarısız.");
                TempData["Error"] = "Talep oluşturulamadı.";
                await SetDropdownsAsync();
                return View(model);
            }

            TempData["Success"] = "Talep başarılı şekilde oluşturuldu.";
            return RedirectToAction("Index");
        }


        private async Task SetDropdownsAsync()
        {
            var categories = await _categoryManager.GetByDefaultsAsync<GetCategoryForSelectListDTO>(
                x => x.Status != Status.Passive
            );

            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.SubCategories = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            ViewBag.Products = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
        }




    }
}
