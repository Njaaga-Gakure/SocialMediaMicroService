using CommentsService.Models.DTOs;

namespace CommentsService.Service.IService
{
    public interface IPost
    {
        Task<PostDTO> GetPostById(Guid postId);
    }
}
