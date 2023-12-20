namespace CommentsService.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        public string UserName {get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Body {  get; set; } = string.Empty;
    }
}
