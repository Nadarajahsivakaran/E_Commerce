
using Auth.Models;
using Auth.Models.DTO;
using AutoMapper;

namespace Auth.DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Register, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
        }
    }
}
