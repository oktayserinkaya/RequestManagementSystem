using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.PurchaseDTO;

namespace WEB.AutoMapper
{
    public class PurchaseMapping : Profile
    {
        public PurchaseMapping()
        {
            CreateMap<Purchase, GetPurchaseForPaymentDTO>();
            CreateMap<CreateOrUpdatePurchaseDTO, Purchase>();
        }
    }
}
