using System.ComponentModel.DataAnnotations;

namespace PostsService.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set;  }
        
        public Guid UserId { get; set; }    
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

    }
}
