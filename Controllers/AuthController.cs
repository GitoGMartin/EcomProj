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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            bool success = await _authService.Login(dto);

            if (!success)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok("Login successful.");
        }
    }
}
