namespace CommentsService.Models.DTOs
{
    public class AddCommentDTO
    {
        public Guid PostId { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
