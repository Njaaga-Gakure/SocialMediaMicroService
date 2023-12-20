using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
