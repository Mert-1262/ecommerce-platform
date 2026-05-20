using ECommerce.Business.Abstract;
using ECommerce.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            string result = _authService.Register(registerDto);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var token = _authService.Login(loginDto);

                return Ok(token);
            }
            catch (Exception ex) when (ex.Message == "Email veya şifre hatalı.")
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}