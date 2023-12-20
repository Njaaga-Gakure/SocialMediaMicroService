using AuthService.Data;
using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Service.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Service
{
    public class UserService : IUser
    {
        private readonly SocialContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWT _jwtService;

        public UserService(SocialContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IJWT jwtService)
        {
            _context = context; 
            _mapper = mapper;   
            _userManager = userManager; 
            _roleManager = roleManager; 
            _jwtService = jwtService;   
        }
        public async Task<string> AddUser(RegisterUserDTO newUser)
        {
            var user = _mapper.Map<User>(newUser);
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (result.Succeeded)
            {
                var usersCount = _context.Users.Count();
                var role = usersCount == 1 ? "Admin" : "User";
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                   
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(user, role);
                return string.Empty;    
            }
            return result.Errors.FirstOrDefault().Description;
        }

        public async Task<LoginResponseDTO> LoginUser(LoginUserDTO loginUser)
        {
           var user = await _context.Users.Where(user => user.Email == loginUser.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return new LoginResponseDTO();
            }
            var isValidPassword = _userManager.CheckPasswordAsync(user, loginUser.Password).GetAwaiter().GetResult();
            if (!isValidPassword)
            {
                return new LoginResponseDTO();
            }
            var responseUser = _mapper.Map<ResponseUserDTO>(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.CreateToken(user, userRoles);
            var response = new LoginResponseDTO()
            {
                 Token = token,
                 User = responseUser,   
            };
            return response;    
        }
    }
}
