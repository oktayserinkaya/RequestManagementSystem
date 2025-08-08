using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.CategoryDTO;

namespace BUSINESS.AutoMapper
{
    public class CategoryBusinessMapping : Profile
    {
        public CategoryBusinessMapping()
        {
            CreateMap<Category, CategorySelectListDTO>();
        }
    }
}
