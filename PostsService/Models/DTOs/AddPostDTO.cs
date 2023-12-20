using System.ComponentModel.DataAnnotations;

namespace PostsService.Models.DTOs
{
    public class AddPostDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;
    }
}
