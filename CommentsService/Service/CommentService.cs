using CommentsService.Data;
using CommentsService.Models;
using CommentsService.Models.DTOs;
using CommentsService.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Service
{
    public class CommentService : IComment
    {
        private readonly CommentsContext _context;

        public CommentService(CommentsContext context)
        {
            _context = context;
        }
        public async Task<string> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return "Comment Added Successfully";
        }


        public async Task<List<Comment>> GetAllComments(Guid postId)
        {
            var comments = await _context.Comments.Where(comment => comment.PostId == postId).ToListAsync(); 
            return comments;    
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            var comment = await _context.Comments.Where(comment => comment.Id == id).FirstOrDefaultAsync();
            return comment;
        }

        public async Task<bool> UpdateComment(Guid id, AddCommentDTO updatePost)
        {
            var comment = await GetCommentById(id); 

            if (comment != null) 
            {
                comment.Body = updatePost.Body; 
                await _context.SaveChangesAsync();
                return true;    
            }
            return false;
        }
        public async Task<bool> DeleteComment(Guid id)
        {
            var comment = await GetCommentById(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();  
                return true;
            }
            return false;
        }
    }
}
