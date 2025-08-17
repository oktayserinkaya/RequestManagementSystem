using System.Globalization;
using AutoMapper;
using DTO.Concrete.RoleDTO;
using WEB.Areas.Admin.Models.RoleVM;

namespace WEB.AutoMapper
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            // DTO -> VM (DTO.UpdatedDate = string, VM.UpdatedDate = DateTime?)
            CreateMap<GetRoleDTO, GetRoleVM>()
                .ForMember(d => d.UpdatedDate, o => o.MapFrom(s =>
                    string.IsNullOrWhiteSpace(s.UpdatedDate) || s.UpdatedDate.Trim() == "-"
                        ? (DateTime?)null
                        : DateTime.Parse(s.UpdatedDate, CultureInfo.GetCultureInfo("tr-TR"))));

            // Create/Update ekranları
            CreateMap<CreateRoleDTO, CreateRoleVM>().ReverseMap();
            CreateMap<UpdateRoleDTO, UpdateRoleVM>().ReverseMap();
        }
    }
}
