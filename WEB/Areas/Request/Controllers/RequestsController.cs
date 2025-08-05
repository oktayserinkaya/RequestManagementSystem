using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Enums;
using CORE.Extensions;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class RequestsController(IRequestManager requestManager, ICategoryManager categoryManager, IMapper mapper, ILogger<RequestsController> logger, IEmployeeManager employeeManager) : Controller
    {
        private readonly IRequestManager _requestManager = requestManager;
        private readonly ICategoryManager _categoryManager = categoryManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<RequestsController> _logger = logger;
        private readonly IEmployeeManager _employeeManager = employeeManager;

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

            // Eğer session boşsa, login'e yönlendir
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(department))
            {
                TempData["Error"] = "Oturum süreniz dolmuş olabilir. Lütfen tekrar giriş yapınız.";
                return RedirectToAction("Login", "Account");
            }

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
            model.FirstName = HttpContext.Session.GetString("FirstName");
            model.LastName = HttpContext.Session.GetString("LastName");
            model.DepartmentName = HttpContext.Session.GetString("Department");

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

            // Employee eşlemesi (TitleId ve EmployeeId için)
            var employee = await _employeeManager.GetByDefaultAsync<CORE.Entities.Concrete.Employee>(
                x => x.FirstName == model.FirstName && x.LastName == model.LastName && x.Department.DepartmentName == model.DepartmentName,
                join: x => x.Include(e => e.Department).Include(e => e.Title)
            );

            if (employee == null)
            {
                _logger.LogWarning("Employee bulunamadı: {FirstName} {LastName}, Departman: {Department}", model.FirstName, model.LastName, model.DepartmentName);
                TempData["Error"] = "Çalışan sistemde bulunamadı.";
                await SetDropdownsAsync();
                return View(model);
            }

            var dto = _mapper.Map<CreateRequestDTO>(model);

            // DTO'dan Entity'ye map + Employee bilgileri
            var entity = _mapper.Map<CORE.Entities.Concrete.Request>(dto);
            entity.EmployeeId = employee.Id;
            entity.TitleId = employee.TitleId;

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

            // 🔍 Log ile kontrol et
            Console.WriteLine($"RequestEntity Id: {entity.Id}, Status: {entity.Status}");
            _logger.LogInformation("Kayıt öncesi kontrol -> Id: {Id}, Status: {Status}", entity.Id, entity.Status);

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
