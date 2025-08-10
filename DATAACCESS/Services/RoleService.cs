using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CORE.Enums;
using CORE.IdentityEntities;
using CORE.Interface;
using DATAACCESS.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DATAACCESS.Services
{
    public class RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : IRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<IdentityResult> AddRoleAsync(AppRole role)=> await _roleManager.CreateAsync(role);
        public async Task<IdentityResult> UpdateRoleAsync(AppRole role)
        {
            role.UpdatedDate = DateTime.Now;
            role.Status = Status.Modified;
            return await _roleManager.UpdateAsync(role);
        }
        public async Task<IdentityResult> DeleteRoleAsync(AppRole role)
        {
            role.UpdatedDate = DateTime.Now;
            role.Status = Status.Passive;
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<AppRole?> GetRoleAsync(Guid roleId)
        => await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id ==roleId && x.Status != Status.Passive);

        public async Task<List<AppRole>> GetRolesAsync()
        => await _roleManager.Roles.Where(x=> x.Status != Status.Passive).ToListAsync();

        public async Task<bool> AnyAsync(Expression<Func<AppRole, bool>> expression)
        => await _roleManager.Roles.AnyAsync(expression);

        public async Task<bool> AnyUserInRole(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if(role == null)
                return false;
            var anyUserInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            return anyUserInRole.Any();
        }    
              

        
    }
}
