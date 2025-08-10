using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Enums;
using CORE.IdentityEntities;
using DTO.Concrete.RoleDTO;

namespace BUSINESS.AutoMapper
{
    public class RoleBusinessMapping : Profile
    {
        public RoleBusinessMapping()
        {
            CreateMap<GetRoleDTO, AppRole>().ReverseMap()
            .ForMember(x => x.UpdatedDate, dest => dest.MapFrom(z => z.UpdatedDate != null ? z.UpdatedDate.ToString() : " - "))
            .ForMember(x => x.Status, dest => dest.MapFrom(z => z.Status == Status.Active ? "Aktif" : "Güncellenmiş"));

            CreateMap<AppRole, CreateRoleDTO>().ReverseMap();
            CreateMap<AppRole, UpdateRoleDTO>().ReverseMap();
        }
    }
}
