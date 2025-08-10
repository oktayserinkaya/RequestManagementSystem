using AutoMapper;
using CORE.Entities.Concrete;
using CORE.Extensions;
using DTO.Concrete.RequestDTO;
using WEB.Areas.Request.Models.RequestVM;

public class RequestMapping : Profile
{
    public RequestMapping()
    {
        CreateMap<CreateRequestVM, CreateRequestDTO>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.SpecialProductName, o => o.MapFrom(s => s.SpecialProductName))
                .ForMember(d => d.RequestDate, o => o.MapFrom(s => s.RequestDate))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.ProductFeaturesFile, o => o.MapFrom(s => s.ProductFeaturesFile));

        CreateMap<UpdateRequestDTO, Request>()
     .ForMember(d => d.CommissionNote, o => o.MapFrom(s => s.Description))
     // dosya yolu null ise mevcut değeri koru
     .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

    }
}
