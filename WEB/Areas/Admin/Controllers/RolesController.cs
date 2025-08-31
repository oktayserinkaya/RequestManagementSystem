using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.IdentityEntities;                 // AppRole
using DTO.Concrete.RoleDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB.Areas.Admin.Models.RoleVM;

namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController(
        IRoleManager roleManager,
        IMapper mapper,
        IUserManager userManager,
        RoleManager<AppRole> aspRoleManager,     // Identity RoleManager
        ILookupNormalizer normalizer             // Invariant normalizer
    ) : Controller
    {
        private readonly RoleManager<AppRole> _aspRoleManager = aspRoleManager;
        private readonly ILookupNormalizer _normalizer = normalizer;

        public async Task<IActionResult> Index()
        {
            var dto = await roleManager.GetRolesAsync();
            var model = mapper.Map<List<GetRoleVM>>(dto);
            return View(model);
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Name = model.Name?.Trim();
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                TempData["Error"] = "Rol adı boş olamaz!";
                return View(model);
            }

            var exists = await roleManager.AnyRoleNameAsync(model.Name);
            if (exists)
            {
                TempData["Error"] = "Bu rol zaten kayıtlıdır!";
                return View(model);
            }

            var dto = mapper.Map<CreateRoleDTO>(model);
            var result = await roleManager.AddRoleAsync(dto);

            if (!result)
            {
                TempData["Error"] = "Rol oluşturulamadı!";
                return View(model);
            }

            TempData["Success"] = "Rol başarıyla kaydedildi!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            var guidResult = Guid.TryParse(id, out Guid entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var roleDto = await roleManager.GetRoleByIdAsync<UpdateRoleDTO>(entityId);
            if (roleDto == null)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var model = mapper.Map<UpdateRoleVM>(roleDto);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(UpdateRoleVM model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Name = model.Name?.Trim();

            var entity = await roleManager.GetRoleByIdAsync<UpdateRoleDTO>(model.Id);
            if (entity == null)
            {
                TempData["Error"] = "Rol bulunamadı!";
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                TempData["Error"] = "Rol adı boş olamaz!";
                return View(model);
            }

            var allRoles = await roleManager.GetRolesAsync();
            var nameClash = allRoles.Any(r =>
                r.Id != model.Id &&
                string.Equals(r.Name?.Trim(), model.Name, StringComparison.OrdinalIgnoreCase));

            if (nameClash)
            {
                TempData["Error"] = "Bu isimde başka bir rol bulunmaktadır!";
                return View(model);
            }

            mapper.Map(model, entity);

            var result = await roleManager.UpdateRoleAsync(entity);
            if (!result)
            {
                TempData["Error"] = "Rol güncellenemedi!";
                return View(model);
            }

            TempData["Success"] = "Rol başarıyla güncellendi!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var guidResult = Guid.TryParse(id, out Guid entityId);
            if (!guidResult)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var hasUserCheck = await roleManager.AnyUserInRole(entityId);
            if (hasUserCheck)
            {
                TempData["Error"] = "Bu rolde kullanıcılar var. Silinemez!";
                return RedirectToAction(nameof(Index));
            }

            var result = await roleManager.DeleteRoleAsync(entityId);
            if (!result)
            {
                TempData["Error"] = "Rol silinemedi!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Rol başarıyla silindi!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignToRole(string id)
        {
            var guidOk = Guid.TryParse(id, out Guid roleId);
            if (!guidOk)
            {
                TempData["Error"] = "Rol bulunamamıştır!";
                return RedirectToAction(nameof(Index));
            }

            var identityRole = await _aspRoleManager.FindByIdAsync(roleId.ToString());
            if (identityRole == null)
            {
                TempData["Error"] = "Rol Identity tarafında bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var normalized = _normalizer.NormalizeName(identityRole.Name);
            if (!string.Equals(identityRole.NormalizedName, normalized, StringComparison.Ordinal))
            {
                identityRole.NormalizedName = normalized;
                var upd = await _aspRoleManager.UpdateAsync(identityRole);
                if (!upd.Succeeded)
                {
                    TempData["Error"] = "Rol normalizasyonu güncellenemedi.";
                    return RedirectToAction(nameof(Index));
                }
            }

            var storedRoleName = identityRole.Name;

            var hasRole = await userManager.GetUsersHasRoleAsync(storedRoleName);
            var hasNotRole = await userManager.GetUsersHasNotRoleAsync(storedRoleName);

            var model = new AssignToRoleVM
            {
                RoleId = roleId,
                RoleName = storedRoleName,
                HasRole = mapper.Map<List<GetUserForRoleVM>>(hasRole),
                HasNotRole = mapper.Map<List<GetUserForRoleVM>>(hasNotRole)
            };

            return View(model);
        }

        // Tek kullanıcıyı role atayan butonlu formun POST'u
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignUserToRole(Guid roleId, string userKey)
        {
            if (roleId == Guid.Empty || string.IsNullOrWhiteSpace(userKey))
            {
                TempData["Error"] = "Geçersiz istek.";
                return RedirectToAction(nameof(Index));
            }

            var identityRole = await _aspRoleManager.FindByIdAsync(roleId.ToString());
            if (identityRole == null)
            {
                TempData["Error"] = "Rol Identity tarafında bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var normalized = _normalizer.NormalizeName(identityRole.Name);
            if (!string.Equals(identityRole.NormalizedName, normalized, StringComparison.Ordinal))
            {
                identityRole.NormalizedName = normalized;
                var upd = await _aspRoleManager.UpdateAsync(identityRole);
                if (!upd.Succeeded)
                {
                    TempData["Error"] = "Rol normalizasyonu güncellenemedi.";
                    return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
                }
            }

            var storedRoleName = identityRole.Name; // UserManager.AddToRoleAsync için isim

            // userKey -> Guid ya da Username olabilir
            Guid userId;
            if (!Guid.TryParse(userKey, out userId))
            {
                var user = await userManager.FindByNameAsync(userKey.Trim());
                if (user == null)
                {
                    TempData["Error"] = "Kullanıcı bulunamadı!";
                    return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
                }
                userId = user.Id;
            }

            var res = await userManager.AddUserToRoleAsync(userId, storedRoleName);
            TempData[res.Succeeded ? "Success" : "Error"] =
                res.Succeeded ? "Kullanıcı role atandı." : "Role atama başarısız.";

            return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
        }

        // Tek kullanıcıyı rolden çıkaran butonlu formun POST'u
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(Guid roleId, string userKey)
        {
            if (roleId == Guid.Empty || string.IsNullOrWhiteSpace(userKey))
            {
                TempData["Error"] = "Geçersiz istek.";
                return RedirectToAction(nameof(Index));
            }

            var identityRole = await _aspRoleManager.FindByIdAsync(roleId.ToString());
            if (identityRole == null)
            {
                TempData["Error"] = "Rol Identity tarafında bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var normalized = _normalizer.NormalizeName(identityRole.Name);
            if (!string.Equals(identityRole.NormalizedName, normalized, StringComparison.Ordinal))
            {
                identityRole.NormalizedName = normalized;
                var upd = await _aspRoleManager.UpdateAsync(identityRole);
                if (!upd.Succeeded)
                {
                    TempData["Error"] = "Rol normalizasyonu güncellenemedi.";
                    return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
                }
            }

            var storedRoleName = identityRole.Name;

            Guid userId;
            if (!Guid.TryParse(userKey, out userId))
            {
                var user = await userManager.FindByNameAsync(userKey.Trim());
                if (user == null)
                {
                    TempData["Error"] = "Kullanıcı bulunamadı!";
                    return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
                }
                userId = user.Id;
            }

            var res = await userManager.RemoveUserFromRoleAsync(userId, storedRoleName);
            TempData[res.Succeeded ? "Success" : "Error"] =
                res.Succeeded ? "Kullanıcı rolden çıkarıldı." : "Kullanıcı rolden çıkarılamadı.";

            return RedirectToAction(nameof(AssignToRole), new { id = roleId.ToString() });
        }

        private async Task<IActionResult> ReloadAssignToRoleView(Guid roleId)
        {
            var identityRole = await _aspRoleManager.FindByIdAsync(roleId.ToString());
            if (identityRole == null)
            {
                TempData["Error"] = "Rol Identity tarafında bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var normalized = _normalizer.NormalizeName(identityRole.Name);
            if (!string.Equals(identityRole.NormalizedName, normalized, StringComparison.Ordinal))
            {
                identityRole.NormalizedName = normalized;
                var upd = await _aspRoleManager.UpdateAsync(identityRole);
                if (!upd.Succeeded)
                {
                    TempData["Error"] = "Rol normalizasyonu güncellenemedi.";
                    return RedirectToAction(nameof(Index));
                }
            }

            var storedRoleName = identityRole.Name;

            var hasRole = await userManager.GetUsersHasRoleAsync(storedRoleName);
            var hasNotRole = await userManager.GetUsersHasNotRoleAsync(storedRoleName);

            var vm = new AssignToRoleVM
            {
                RoleId = roleId,
                RoleName = storedRoleName,
                HasRole = mapper.Map<List<GetUserForRoleVM>>(hasRole),
                HasNotRole = mapper.Map<List<GetUserForRoleVM>>(hasNotRole)
            };

            return View("AssignToRole", vm);
        }
    }
}
