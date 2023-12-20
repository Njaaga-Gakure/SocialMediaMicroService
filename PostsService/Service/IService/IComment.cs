using PostsService.Models.DTOs;

namespace PostsService.Service.IService
{
    public interface IComment
    {
        Task<List<CommentDTO>> GetCommentsOfPost(Guid postId);
    }
}
