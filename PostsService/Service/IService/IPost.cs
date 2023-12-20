using PostsService.Models;
using PostsService.Models.DTOs;

namespace PostsService.Service.IService
{
    public interface IPost
    {
        Task<string> CreatePost(Post post);

        Task<List<Post>> GetAllPosts();

        Task<Post> GetPostById(Guid id);
        Task<bool> UpdatePost(Guid id, AddPostDTO updatePost);
        Task<bool> DeletePost(Guid id);
    }
}
