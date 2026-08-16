using ECommerce.API.Interfaces;
using EcomProj.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcomProj.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] User user)
        //{
        //    if (user is null) return BadRequest();
        //    var newId = await _userRepository.CreateAsync(user);
        //    if (newId == Guid.Empty) return StatusCode(500, "Could not create user");
        //    return CreatedAtAction(nameof(Get), new { id = newId }, null);
        //}

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDTO user)
        {
            if (user is null) return BadRequest();
            var ok = await _userRepository.UpdateAsync(id, user);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _userRepository.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}