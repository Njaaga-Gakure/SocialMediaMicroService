using AutoMapper;
using CommentsService.Models;
using CommentsService.Models.DTOs;

namespace CommentsService.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentDTO, Comment>().ReverseMap();
        }
    }
}
