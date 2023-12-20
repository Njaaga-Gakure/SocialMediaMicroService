namespace PostsService.Models.DTOs
{
    public class PostResponseDTO
    {
        public Guid PostId { get; set; }

        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();   
    }
}
