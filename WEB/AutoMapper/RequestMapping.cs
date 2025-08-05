using AutoMapper;
using CORE.Entities.Concrete;
using CORE.Extensions;
using DTO.Concrete.RequestDTO;
using WEB.Areas.Request.Models.RequestVM;

public class RequestMapping : Profile
{
    public RequestMapping()
    {
        CreateMap<GetRequestsVM, Request>()
            .ReverseMap()
            .ForMember(dest => dest.Status, src => src.MapFrom(z => z.Status.GetDisplayName()))
            .ForMember(dest => dest.UpdatedDate, src => src.MapFrom(z => z.UpdatedDate.HasValue ? z.UpdatedDate.Value.ToString() : " - "));

        CreateMap<CreateRequestVM, CreateRequestDTO>().ReverseMap();


        CreateMap<CreateRequestDTO, Request>()
    .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()) // çünkü bu backend'de set edilmeli
    .ForMember(dest => dest.TitleId, opt => opt.Ignore());   // aynısı


        CreateMap<Category, GetCategoryForSelectListDTO>().ReverseMap();
        CreateMap<SubCategory, GetSubCategoryForSelectListDTO>().ReverseMap();
        CreateMap<Product, GetProductForSelectListDTO>().ReverseMap();
    }
}
