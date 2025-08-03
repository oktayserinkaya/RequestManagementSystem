using AutoMapper;
using CORE.Entities.Concrete;
using CORE.Extensions;
using DTO.Concrete.RequestDTO;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.AutoMapper
{
    public class RequestMapping : Profile
    {
        public RequestMapping()
        {
            CreateMap<GetRequestsVM, Request>().ReverseMap().ForMember(dest => dest.Status, src => src.MapFrom(z => z.Status.GetDisplayName())).ForMember(dest => dest.UpdatedDate, src => src.MapFrom(z => z.UpdatedDate.HasValue ? z.UpdatedDate.Value.ToString() : " - "));

            CreateMap<CreateRequestVM, CreateRequestDTO>().ReverseMap();
            CreateMap<Category, GetCategoryForSelectListDTO>().ReverseMap();
            CreateMap<SubCategory, GetSubCategoryForSelectListDTO>().ReverseMap();
            CreateMap<Product, GetProductForSelectListDTO>().ReverseMap();
        }
    }
}
