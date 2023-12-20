using AuthService.Models;
using AuthService.Models.DTOs;
using AutoMapper;

namespace AuthService.Profiles
{
    public class SocialProfiles: Profile
    {
        public SocialProfiles()
        {
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, ResponseUserDTO>().ReverseMap();
        }
    }
}
