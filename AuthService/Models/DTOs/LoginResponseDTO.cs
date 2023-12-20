namespace AuthService.Models.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public ResponseUserDTO User { get; set; } = new ResponseUserDTO(); 
    }
}
