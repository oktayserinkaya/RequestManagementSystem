using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CORE.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace CORE.Interface
{
    public interface IRoleRepository
    {
        Task<List<AppRole>> GetRolesAsync();
        Task<AppRole?> GetRoleAsync(Guid roleId);
        Task<bool> AnyUserInRole(Guid roleId);
        Task<bool> AnyAsync(Expression<Func<AppRole, bool>> expression);
        Task<IdentityResult> AddRoleAsync(AppRole role);
        Task<IdentityResult> UpdateRoleAsync(AppRole role);
        Task<IdentityResult> DeleteRoleAsync(AppRole role);
    }
}
