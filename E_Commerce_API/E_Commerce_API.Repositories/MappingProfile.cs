using AutoMapper;
using Product.Models;
using Product.Models.DTO;

namespace Product.DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Category, CategoryDTO>()
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id)).ReverseMap();             CreateMap<CreatedCategoryDTO, Category>();
        }
    }
}
