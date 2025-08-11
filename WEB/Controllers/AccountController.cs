using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.IdentityEntities;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.EmployeeDTO;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB.Models.ViewModels.AccountVM;

namespace WEB.Controllers
{
    [Route("Account")] // -> /Account/...
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IMapper _mapper;

        public AccountController(
            IUserManager userManager,
            IEmployeeManager employeeManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _employeeManager = employeeManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("~/")]
        [HttpGet("Login")] // GET /Account/Login

        public async Task<IActionResult> Login()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
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
        [HttpPost("Login")] // POST /Account/Login
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = _mapper.Map<LoginDTO>(model);

            var signIn = await _userManager.LoginAsync(dto);
            if (!signIn.Succeeded)
            {
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                return View(model);
            }

            var username = dto.Username;
            var userEntity = await _userManager.FindByNameAsync(username);
            if (userEntity is not null && userEntity.HasFirstPasswordChanged == false)
            {
                TempData["Error"] = "İlk kez giriş: e-postadaki linkten şifrenizi değiştiriniz.";
                await _userManager.LogoutAsync();
                return RedirectToAction(nameof(Login));
            }

            if (await _userManager.IsUserInRoleAsync(username, "Admin"))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            if (await _userManager.IsUserInRoleAsync(username, "TalepOluşturanBirim"))
                return RedirectToAction("MyRequests", "Requests", new { area = "Request" });

            if (await _userManager.IsUserInRoleAsync(username, "IhtiyacTespitKomisyonu"))
                return RedirectToAction("Index", "RequestEvaluation", new { area = "RequestEvaluation" });

            if (await _userManager.IsUserInRoleAsync(username, "SatinAlmaBirimi"))
                return RedirectToAction("Index", "Orders", new { area = "PurchaseTransaction" });

            if (await _userManager.IsUserInRoleAsync(username, "DepoBirimi"))
                return RedirectToAction("Index", "Stock", new { area = "Warehouse" });

            if (await _userManager.IsUserInRoleAsync(username, "OdemeBirimi"))
                return RedirectToAction("Index", "Payments", new { area = "Finance" });

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost("Logout")] // POST /Account/Logout
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userManager.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet("EditUser")]
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

        [Authorize]
        [HttpPost("EditUser")]
        [ValidateAntiForgeryToken]
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

            TempData["Success"] = "Kullanıcı bilgileri güncellendi.";
            return View(model);
        }

        [Authorize]
        [HttpGet("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var userId = await _userManager.GetUserIdByClaimsAsync(User);
            var model = new ChangedPasswordVM { Id = userId };
            return View(model);
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangedPasswordVM model)
        {
            var dto = _mapper.Map<ChangePasswordDTO>(model);
            var result = await _userManager.ChangePassswordAsync(dto);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Eski şifre hatalı!";
                return View(model);
            }

            TempData["Success"] = "Şifre değiştirildi.";
            await _userManager.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        [HttpGet("AccessDenied")] // GET /Account/AccessDenied
        public IActionResult AccessDenied() => View();
    }
}
