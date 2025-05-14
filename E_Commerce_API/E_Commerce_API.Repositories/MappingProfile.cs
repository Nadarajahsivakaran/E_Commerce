using AutoMapper;
using E_Commerce_API.Models.DTO;
using Product_API.Models;
using Product_API.Models.DTO;


namespace E_Commerce_API.DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Category, CategoryDTO>()
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));
            CreateMap<CategoryDTO, Category>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId));
            CreateMap<CreatedCategoryDTO, Category>();
        }
    }
}
