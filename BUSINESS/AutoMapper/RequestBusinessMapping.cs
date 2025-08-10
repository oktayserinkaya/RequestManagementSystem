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
            CreateMap<CreateRequestDTO, Request>()
                // DTO -> Entity alanları (varsa isim eşleşmesi otomatik ama açık yazmak netlik sağlar)
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.SpecialProductName, o => o.MapFrom(s => s.SpecialProductName))
                .ForMember(d => d.RequestDate, o => o.MapFrom(s => s.RequestDate))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.ProductFeatures, o => o.MapFrom(s => s.Description))
                // kaynakta olup destination'a gitmeyecek alanlar
                .ForSourceMember(s => s.ProductFeaturesFile, o => o.DoNotValidate())

                // === DTO’da olmayan tüm entity alanlarını IGNORE et ===
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.Status, o => o.Ignore())

                .ForMember(d => d.AppUserId, o => o.Ignore()) // controller’da set ediliyor
                .ForMember(d => d.DepartmentId, o => o.Ignore()) // controller’da set ediliyor

                .ForMember(d => d.EmployeeId, o => o.Ignore())
                .ForMember(d => d.Employee, o => o.Ignore())
                .ForMember(d => d.TitleId, o => o.Ignore())
                .ForMember(d => d.Title, o => o.Ignore())

                .ForMember(d => d.Product, o => o.Ignore())
                .ForMember(d => d.ProductFeaturesFilePath, o => o.Ignore())

                .ForMember(d => d.Payments, o => o.Ignore())
                .ForMember(d => d.Warehouses, o => o.Ignore());

            CreateMap<UpdateRequestDTO, Request>()
            .ForMember(d => d.Id, o => o.Ignore()) // id parametreden geliyor
            .ForMember(d => d.UpdatedDate, o => o.Ignore()) // BaseService zaten set ediyor
            .ForMember(d => d.Status, o => o.Ignore());     // BaseService UpdateAsync set ediyor
        }
    }
}

