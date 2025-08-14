using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.PurchaseDTO;

namespace BUSINESS.AutoMapper
{
    public class PurchaseBusinessMapping : Profile
    {
        public PurchaseBusinessMapping()
        {
            CreateMap<Purchase, GetPurchaseForPaymentDTO>();
            CreateMap<CreateOrUpdatePurchaseDTO, Purchase>();
        }
    }
}
