using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CORE.IdentityEntities;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace BUSINESS.Manager.Interface
{
    public interface IUserManager
    {
        
        Task<List<GetUserForRoleDTO>> GetUsersHasRoleAsync(string roleName);
        Task<List<GetUserForRoleDTO>> GetUsersHasNotRoleAsync(string roleName);
        Task<List<string>> GetUserNamesHasRoleAsync(string roleName);
        Task<GetUserDTO?> FindUserByIdAsync(Guid userId);
        Task<GetUserDTO?> FindUserByEmailAsync(string email);
        Task<GetUserDTO?> FindUserByUsernameAsync(string userName);
        Task<T?> FindUserByClaimsAsync<T>(ClaimsPrincipal user);
        Task<Guid> GetUserIdByClaimsAsync(ClaimsPrincipal user);

        Task<AppUser?> FindByNameAsync(string username);

       
        Task<bool> AnyAsync(Expression<Func<AppUser, bool>> expression);

        
        Task<IdentityResult> AddUserToRoleAsync(Guid userId, string roleName);
        Task<IdentityResult> AddUserToRoleAsync(string email, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(Guid userId, string roleName);
        Task<bool> IsUserInRoleAsync(string userName, string roleName);

       
        Task<IdentityResult> CreateUserAsync(CreateUserDTO user);
        Task<IdentityResult> UpdateUserAsync(EditUserDTO user);
        Task<IdentityResult> UpdateUserAsync(GetUserDTO user);
        Task<IdentityResult> ChangePassswordAsync(ChangePasswordDTO dto);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO dto);

   
        Task<string> GenerateTokenForResetPasswordAsync(string email);
        Task<bool> IsTokenValidAsync(string email, string token);

        Task<SignInResult> LoginAsync(LoginDTO dto);
        Task LogoutAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
