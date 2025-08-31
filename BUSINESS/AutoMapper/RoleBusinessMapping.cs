using System;
using System.Collections.Generic;
using System.Globalization;
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
            CreateMap<AppRole, GetRoleDTO>()
                .ForMember(d => d.UpdatedDate, o => o.MapFrom(s =>
                    s.UpdatedDate.HasValue
                        ? s.UpdatedDate.Value.ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"))
                        : " - "))
                .ForMember(d => d.Status, o => o.MapFrom(s =>
                    s.Status == Status.Active
                        ? "Aktif"
                        : (s.Status == Status.Modified ? "Güncellenmiş" : "Pasif")));

            CreateMap<GetRoleDTO, AppRole>()
                .ForMember(d => d.Status, o => o.MapFrom(s =>
                    (s.Status != null && s.Status.Trim() == "Aktif")
                        ? Status.Active
                        : ((s.Status != null && s.Status.Trim() == "Güncellenmiş")
                            ? Status.Modified
                            : Status.Passive)))
                .ForMember(d => d.UpdatedDate, o => o.MapFrom(s =>
                    string.IsNullOrWhiteSpace(s.UpdatedDate) || s.UpdatedDate.Trim() == "-"
                        ? (DateTime?)null
                        : DateTime.Parse(s.UpdatedDate, CultureInfo.GetCultureInfo("tr-TR"))));

            CreateMap<AppRole, CreateRoleDTO>().ReverseMap();
            CreateMap<AppRole, UpdateRoleDTO>().ReverseMap();
        }
    }
}
