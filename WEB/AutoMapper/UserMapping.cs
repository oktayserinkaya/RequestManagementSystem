using AutoMapper;
using DTO.Concrete.AccountDTO;
using DTO.Concrete.UserDTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WEB.Areas.Admin.Models.RoleVM;
using WEB.Models.ViewModels.AccountVM;

namespace WEB.AutoMapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<LoginVM, LoginDTO>().ReverseMap();
            CreateMap<EditUserVM, EditUserDTO>().ReverseMap();
            CreateMap<ChangedPasswordVM, ChangePasswordDTO>().ReverseMap();
            CreateMap<ResetPasswordVM, ResetPasswordDTO>().ReverseMap();
            CreateMap<GetUserForRoleVM, GetUserForRoleDTO>().ReverseMap();
                        
        }
    }
}
