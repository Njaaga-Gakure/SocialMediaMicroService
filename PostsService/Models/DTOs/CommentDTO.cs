namespace PostsService.Models.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
