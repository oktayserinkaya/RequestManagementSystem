using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.EmployeeDTO;

namespace BUSINESS.AutoMapper
{
    public class EmployeeBusinessMapping : Profile
    {
        public EmployeeBusinessMapping()
        {
            // BUSINESS.AutoMapper/EmployeeBusinessMapping.cs
            CreateMap<Employee, GetEmployeeDTO>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.DepartmentName : null))
                .ForMember(d => d.TitleName, o => o.MapFrom(s => s.Title != null ? s.Title.TitleName : null));

        }
    }
}
