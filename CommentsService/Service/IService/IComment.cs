using CommentsService.Models;
using CommentsService.Models.DTOs;

namespace CommentsService.Service.IService
{
    public interface IComment
    {
        Task<string> CreateComment(Comment comment);

        Task<List<Comment>> GetAllComments(Guid postId);

        Task<Comment> GetCommentById(Guid id);
        Task<bool> UpdateComment(Guid id, AddCommentDTO updatePost);
        Task<bool> DeleteComment(Guid id);
    }
}
