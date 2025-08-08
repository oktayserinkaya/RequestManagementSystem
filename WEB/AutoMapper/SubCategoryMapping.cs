using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.CategoryDTO;
using DTO.Concrete.SubCategoryDTO;

namespace WEB.AutoMapper
{
    public class SubCategoryMapping : Profile
    {
        public SubCategoryMapping()
        {
            CreateMap<SubCategory, SubCategorySelectListDTO>()
             .ForMember(d => d.Name, o => o.MapFrom(s => s.SubCategoryName));
        }
    }
}
