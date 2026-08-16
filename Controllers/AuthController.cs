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
        public async Task<IActionResult> Register(CreateUserDTO dto)
        {
            Guid userId = await _authService.RegisterAsync(dto);

            if (userId == Guid.Empty)
            {
                return Conflict("A user with that email already exists.");
            }

            return Ok(userId);
        }
    }
}
