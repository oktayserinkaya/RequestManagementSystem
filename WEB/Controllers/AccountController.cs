using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using CORE.IdentityEntities;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.CategoryDTO;
using DTO.Concrete.DepatmentDTO;
using DTO.Concrete.EmployeeDTO;
using DTO.Concrete.PaymentDTO;
using DTO.Concrete.ProductDTO;
using DTO.Concrete.RequestDTO;
using DTO.Concrete.SubCategoryDTO;
using DTO.Concrete.UserDTO;
using DTO.Concrete.WarehouseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB.ActionFilters;
using WEB.Areas.Request.Models.RequestVM;
using WEB.Models.ViewModels.AccountVM;

namespace WEB.Controllers
{
    public class AccountController(
        IUserManager userManager,
        IRequestManager requestManager,
        IDepartmentManager departmentManager,
        IWarehouseManager warehouseManager,
        IPaymentManager paymentManager,
        IEmployeeManager employeeManager,
        ICategoryManager categoryManager,
        ISubCategoryManager subCategoryManager,
        IProductManager productManager,
        IMapper mapper
    ) : Controller
    {
        private readonly IUserManager _userManager = userManager;
        private readonly IRequestManager _requestManager = requestManager;
        private readonly IDepartmentManager _departmentManager = departmentManager;
        private readonly IWarehouseManager _warehouseManager = warehouseManager;
        private readonly IPaymentManager _paymentManager = paymentManager;
        private readonly IEmployeeManager _employeeManager = employeeManager;
        private readonly ICategoryManager _categoryManager = categoryManager;
        private readonly ISubCategoryManager _subCategoryManager = subCategoryManager;
        private readonly IProductManager _productManager = productManager;
        private readonly IMapper _mapper = mapper;

        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var dto = _mapper.Map<LoginDTO>(model);
            var result = await _userManager.LoginAsync(dto);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                return View(model);
            }

            var appUser = await _userManager.FindUserByClaimsAsync<GetUserDTO>(User);

            if (!appUser.HasFirstPasswordChanged)
            {
                TempData["Error"] = "İlk kez giriş yaptığınız için e-postanıza gelen linkten şifrenizi değiştiriniz!";
                await _userManager.LogoutAsync();
                return RedirectToAction(nameof(Login));
            }

            // Rol yönlendirmeleri
            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "Admin"))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "TalepOluşturanBirim"))
                return RedirectToAction("Index", "Requests", new { area = "Request" });

            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "IhtiyacTespitKomisyonu"))
                return RedirectToAction("Evaluate", "Requests", new { area = "Commission" });

            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "SatinAlmaBirimi"))
                return RedirectToAction("Approved", "Requests", new { area = "Purchase" });

            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "DepoBirimi"))
                return RedirectToAction("Index", "Warehouses", new { area = "Warehouse" });

            if (await _userManager.IsUserInRoleAsync(appUser.UserName, "OdemeBirimi"))
                return RedirectToAction("Index", "Payments", new { area = "Finance" });

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userManager.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> EditUser()
        {
            var dto = await _userManager.FindUserByClaimsAsync<EditUserDTO>(User);
            if (dto == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction(nameof(Login));
            }

            var model = _mapper.Map<EditUserVM>(dto);

            var employee = await _employeeManager.GetByDefaultAsync<GetEmployeeDTO>(x => x.AppUserId == model.Id);

            if (await _userManager.IsUserInRoleAsync(dto.Username, "Admin"))
            {
                model.FirstName = "Admin";
                model.LastName = "Yönetici";
            }
            else if (employee != null)
            {
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
            }
            else if (await _userManager.IsUserInRoleAsync(dto.Username, "IhtiyacTespitKomisyonu"))
            {
                model.FirstName = "Komisyon";
                model.LastName = "Üyesi";
            }
            else if (await _userManager.IsUserInRoleAsync(dto.Username, "SatinAlmaBirimi"))
            {
                model.FirstName = "Satınalma";
                model.LastName = "Görevlisi";
            }
            else if (await _userManager.IsUserInRoleAsync(dto.Username, "DepoBirimi"))
            {
                model.FirstName = "Depo";
                model.LastName = "Kullanıcısı";
            }
            else if (await _userManager.IsUserInRoleAsync(dto.Username, "OdemeBirimi"))
            {
                model.FirstName = "Ödeme";
                model.LastName = "Yetkilisi";
            }

            return View(model);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            if (await _userManager.AnyAsync(x => x.UserName == model.UserName && x.Id != model.Id))
            {
                TempData["Error"] = "Bu kullanıcı adı zaten kullanılıyor!";
                return RedirectToAction(nameof(EditUser));
            }

            if (await _userManager.AnyAsync(x => x.Email == model.Email && x.Id != model.Id))
            {
                TempData["Error"] = "Bu e-posta zaten kullanılıyor!";
                return RedirectToAction(nameof(EditUser));
            }

            var dto = _mapper.Map<EditUserDTO>(model);
            var result = await _userManager.UpdateUserAsync(dto);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Kullanıcı güncellenemedi!";
                return RedirectToAction(nameof(EditUser));
            }

            TempData["Success"] = "Kullanıcı bilgileri başarıyla güncellendi.";
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var model = new ChangedPasswordVM { Id = userId };
            return View(model);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangedPasswordVM model)
        {
            var dto = _mapper.Map<ChangePasswordDTO>(model);
            var result = await _userManager.ChangePassswordAsync(dto);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Eski şifre hatalı!";
                return View(model);
            }

            TempData["Success"] = "Şifreniz başarıyla değiştirildi.";
            await _userManager.LogoutAsync();
            return RedirectToAction(nameof(Login));
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


        public IActionResult AccessDenied() => View();
    }
}
