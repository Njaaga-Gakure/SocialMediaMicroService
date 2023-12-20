using AuthService.Models;

namespace AuthService.Service.IService
{
    public interface IJWT
    {
        string CreateToken(User user, IEnumerable<string> roles);
    }
}
