using AuthService.Models.DTOs;

namespace AuthService.Service.IService
{
    public interface IUser
    {
        Task<string> AddUser(RegisterUserDTO newUser);
        Task<LoginResponseDTO> LoginUser(LoginUserDTO loginUser);

        //Task<bool> AssignRole(string Email, string RoleName);
    }
}
