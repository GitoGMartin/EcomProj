using Microsoft.AspNetCore.Mvc;

namespace EcomProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "Healthy",
                message = "E-Commerce API is running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
