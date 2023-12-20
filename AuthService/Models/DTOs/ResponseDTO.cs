namespace AuthService.Models.DTOs
{
    public class ResponseDTO
    {
        public string ErrorMessage { get; set; } = string.Empty;

        public object Result { get; set; } = default!;
    }
}
