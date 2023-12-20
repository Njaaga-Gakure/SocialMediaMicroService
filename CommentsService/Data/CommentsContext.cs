using CommentsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Data
{
    public class CommentsContext : DbContext 
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base (options){}

        public DbSet<Comment> Comments { get; set; }
    }
}
