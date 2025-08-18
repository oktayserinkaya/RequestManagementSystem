using CORE.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RoleFixController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ILookupNormalizer _normalizer;

    public RoleFixController(RoleManager<AppRole> roleManager, ILookupNormalizer normalizer)
    {
        _roleManager = roleManager;
        _normalizer = normalizer;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Normalize()
    {
        var roles = _roleManager.Roles.ToList();
        foreach (var role in roles)
        {
            var normalized = _normalizer.NormalizeName(role.Name);
            if (role.NormalizedName != normalized)
            {
                role.NormalizedName = normalized;
                var r = await _roleManager.UpdateAsync(role);
                if (!r.Succeeded)
                {
                    TempData["Error"] = $"'{role.Name}' için normalize güncellenemedi: {string.Join(", ", r.Errors.Select(e => e.Description))}";
                    return RedirectToAction("Index", "Roles", new { area = "Admin" });
                }
            }
        }

        TempData["Success"] = "Tüm roller normalize edildi.";
        return RedirectToAction("Index", "Roles", new { area = "Admin" });
    }
}
