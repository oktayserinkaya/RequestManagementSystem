using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.ProductDTO;

namespace BUSINESS.AutoMapper
{
    public class ProductBusinessMapping : Profile
    {
        public ProductBusinessMapping()
        {
            CreateMap<Product, ProductSelectListDTO>()
    .ForMember(d => d.Name, o => o.MapFrom(s => s.ProductName))
    .ForMember(d => d.SubCategoryId, o => o.MapFrom(s => s.SubCategoryId));
        }
    }
}
