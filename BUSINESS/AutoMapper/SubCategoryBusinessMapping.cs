using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.CategoryDTO;
using DTO.Concrete.SubCategoryDTO;

namespace BUSINESS.AutoMapper
{
    public class SubCategoryBusinessMapping : Profile
    {
        public SubCategoryBusinessMapping()
        {
            CreateMap<SubCategory, SubCategorySelectListDTO>()
    .ForMember(d => d.Name, o => o.MapFrom(s => s.SubCategoryName));

        }
    }
}
