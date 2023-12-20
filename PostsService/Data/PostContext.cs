using Microsoft.EntityFrameworkCore;
using PostsService.Models;

namespace PostsService.Data
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options) {}
        public DbSet<Post> Posts { get; set; }
    }
}
