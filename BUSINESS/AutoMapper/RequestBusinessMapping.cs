using System; // Activator için
using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.RequestDTO;

namespace BUSINESS.AutoMapper
{
    public class RequestBusinessMapping : Profile
    {
        public RequestBusinessMapping()
        {
            // DTO -> Entity (Create)
            CreateMap<CreateRequestDTO, Request>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.SpecialProductName, o => o.MapFrom(s => s.SpecialProductName))
                .ForMember(d => d.RequestDate, o => o.MapFrom(s => s.RequestDate))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                // Entity'de Description yoksa, açıklamayı ProductFeatures'a yazıyoruz
                .ForMember(d => d.ProductFeatures, o => o.MapFrom(s => s.Description))
                // Dosya içeriği burada işlenmiyor; path'i servis/controller set eder
                .ForSourceMember(s => s.ProductFeaturesFile, o => o.DoNotValidate())

                // DTO'da olmayan entity alanlarını IGNORE et
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())
                .ForMember(d => d.Status, o => o.Ignore())
                .ForMember(d => d.AppUserId, o => o.Ignore())
                .ForMember(d => d.DepartmentId, o => o.Ignore())
                .ForMember(d => d.EmployeeId, o => o.Ignore())
                .ForMember(d => d.Employee, o => o.Ignore())
                .ForMember(d => d.TitleId, o => o.Ignore())
                .ForMember(d => d.Title, o => o.Ignore())
                .ForMember(d => d.Product, o => o.Ignore())
                .ForMember(d => d.ProductFeaturesFilePath, o => o.Ignore())
                .ForMember(d => d.Payments, o => o.Ignore())
                .ForMember(d => d.Warehouses, o => o.Ignore());

            // DTO -> Entity (Update)
            CreateMap<UpdateRequestDTO, Request>()
                // PK / FK scalar alanlar yazılmasın
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.AppUserId, o => o.Ignore())
                .ForMember(d => d.EmployeeId, o => o.Ignore())
                .ForMember(d => d.DepartmentId, o => o.Ignore())
                .ForMember(d => d.TitleId, o => o.Ignore())
                .ForMember(d => d.ProductId, o => o.Ignore())

                // Timestamp'ler servis tarafında yönetiliyor
                .ForMember(d => d.CreatedDate, o => o.Ignore())
                .ForMember(d => d.UpdatedDate, o => o.Ignore())

                // 🔹 ARTIK MAP'LİYORUZ: Komisyon onayı için Status güncellensin
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))

                // Komisyon notu
                .ForMember(d => d.CommissionNote, o => o.MapFrom(s =>
                    string.IsNullOrWhiteSpace(s.CommissionNote) ? s.Description : s.CommissionNote))

                // NULL ve value-type default (Guid.Empty/0/false) değerleri yazma
                .ForAllMembers(o => o.Condition((src, dest, srcMember, ctx) =>
                {
                    if (srcMember == null) return false;
                    var t = srcMember.GetType();
                    if (t.IsValueType && Equals(srcMember, Activator.CreateInstance(t))) return false;
                    return true;
                }));
        }
    }
}
