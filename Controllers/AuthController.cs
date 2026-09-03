using EcomProj.DTOs;
using EcomProj.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcomProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO dto)
        {
            Guid userId = await _authService.RegisterAsync(dto);

            if (userId == Guid.Empty)
            {
                return Conflict("A user with that email already exists.");
            }

            return Ok(userId);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            bool isAuthenticated = await _authService.Login(dto);

            if (!isAuthenticated)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok("Login successful.");
        }
    }
}
