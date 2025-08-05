using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.RequestDTO;

namespace BUSINESS.AutoMapper
{
    public class RequestBusinessMapping : Profile
    {
        public RequestBusinessMapping()
        {
            CreateMap<Request, CreateRequestDTO>().ReverseMap();
            CreateMap<Category, GetCategoryForSelectListDTO>().ReverseMap();
            CreateMap<SubCategory, GetSubCategoryForSelectListDTO>().ReverseMap();
            CreateMap<Product, GetProductForSelectListDTO>().ReverseMap();




        }
    }
}
