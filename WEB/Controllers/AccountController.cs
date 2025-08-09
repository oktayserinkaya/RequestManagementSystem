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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                // Zaten girişli kullanıcıyı rolüne göre yönlendir
                var userName = User.Identity!.Name!;
                if (await _userManager.IsUserInRoleAsync(userName, "Admin"))
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                if (await _userManager.IsUserInRoleAsync(userName, "TalepOluşturanBirim"))
                    return RedirectToAction("MyRequests", "Requests", new { area = "Request" });

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = _mapper.Map<LoginDTO>(model);

            // Giriş
            var signIn = await _userManager.LoginAsync(dto);
            if (!signIn.Succeeded)
            {
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                return View(model);
            }

            // NOT: Aynı request içinde HttpContext.User'ı kullanma.
            // Rol ve ilk şifre kontrolünü username ile yap.
            var username = dto.Username; // sizde "Username" ise: var username = dto.Username;

            // İlk şifre değişimi zorunluluğu (AppUser.HasFirstPasswordChanged)
            var userEntity = await _userManager.FindByNameAsync(username);
            if (userEntity is not null && userEntity.HasFirstPasswordChanged == false)
            {
                TempData["Error"] = "İlk kez giriş yaptığınız için e-postanıza gelen linkten şifrenizi değiştiriniz!";
                await _userManager.LogoutAsync();
                return RedirectToAction(nameof(Login));
            }

            // Rol bazlı yönlendirme
            if (await _userManager.IsUserInRoleAsync(username, "Admin"))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            if (await _userManager.IsUserInRoleAsync(username, "TalepOluşturanBirim"))
                return RedirectToAction("MyRequests", "Requests", new { area = "Request" });

            if (await _userManager.IsUserInRoleAsync(username, "IhtiyacTespitKomisyonu"))
                return RedirectToAction("Index", "Review", new { area = "Commission" });

            if (await _userManager.IsUserInRoleAsync(username, "SatinAlmaBirimi"))
                return RedirectToAction("Index", "Orders", new { area = "Procurement" });

            if (await _userManager.IsUserInRoleAsync(username, "DepoBirimi"))
                return RedirectToAction("Index", "Stock", new { area = "Warehouse" });

            if (await _userManager.IsUserInRoleAsync(username, "OdemeBirimi"))
                return RedirectToAction("Index", "Payments", new { area = "Finance" });

            // Varsayılan
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userManager.LogoutAsync();
            return RedirectToAction("Login", "Account");
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

       

        public IActionResult AccessDenied() => View();
    }
}
