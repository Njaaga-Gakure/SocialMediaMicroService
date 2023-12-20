using AutoMapper;
using PostsService.Models;
using PostsService.Models.DTOs;

namespace PostsService.Profiles
{
    public class PostProfiles : Profile
    {
        public PostProfiles()
        {
            CreateMap<AddPostDTO, Post>().ReverseMap(); 
        }
    }
}
