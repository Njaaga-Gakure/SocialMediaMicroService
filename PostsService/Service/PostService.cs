using Microsoft.EntityFrameworkCore;
using PostsService.Data;
using PostsService.Models;
using PostsService.Models.DTOs;
using PostsService.Service.IService;

namespace PostsService.Service
{
    public class PostService : IPost
    {
        private readonly PostContext _context;

        public PostService(PostContext context)
        {
            _context = context; 
        }
        public async Task<string> CreatePost(Post post)
        {
           await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync(); 
           return "Post Created Successfully";    
        }


        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostById(Guid id)
        {
            var post = await _context.Posts.Where(post => post.Id == id).FirstOrDefaultAsync();
            return post;
        }

        public async Task<bool> UpdatePost(Guid id, AddPostDTO updatePost)
        {
            var post = await GetPostById(id);
            if (post != null)
            { 
                post.Title = updatePost.Title;
                post.Body = updatePost.Body;    
                await _context.SaveChangesAsync();
                return true;
            }
            return false;   
        }
        public async Task<bool> DeletePost(Guid id)
        {
            var post = await GetPostById(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
