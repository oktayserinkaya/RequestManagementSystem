using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.ProductDTO;

namespace WEB.AutoMapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductSelectListDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ProductName));
        }
    }
}
