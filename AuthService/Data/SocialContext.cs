using AuthService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class SocialContext: IdentityDbContext<User>
    {
        public SocialContext(DbContextOptions<SocialContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
