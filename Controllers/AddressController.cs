using ECommerce.API.Models;
using EcomProj.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcomProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var users = await _addressRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _addressRepository.GetAddressByIdAsync(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Address addy)
        {
            if (addy is null) return BadRequest();
            var ok = await _addressRepository.UpdateAsync(id, addy);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _addressRepository.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

    }
}
