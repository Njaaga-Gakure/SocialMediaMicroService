using AuthService.Models.DTOs;
using AuthService.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDTO _response;

        public AuthController(IUser userService)
        {
            _response = new ResponseDTO();  
            _userService = userService; 
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> Register(RegisterUserDTO newUser)
        {
            try
            {
                var response = await _userService.AddUser(newUser);

                if (response == string.Empty)
                {
                    _response.Result = "User Created Successfully :)";
                    return Created("", _response);
                }
                _response.ErrorMessage = response;
                return BadRequest(_response);
            }
            catch (Exception ex) 
            { 
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginUserDTO loginUser)
        {
            try
            {
                var response = await _userService.LoginUser(loginUser);

                if (string.IsNullOrWhiteSpace(response.Token))
                {
                    _response.ErrorMessage = "Invalid Credentials :(";
                    _response.Result = response;
                    return BadRequest(_response);
                }
                _response.Result = response;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
