using AutoMapper;
using CORE.Entities.Concrete;
using DTO.Concrete.CategoryDTO;

namespace WEB.AutoMapper
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategorySelectListDTO>()
           .ForMember(d => d.Name, o => o.MapFrom(s => s.CategoryName));
        }
    }
}
