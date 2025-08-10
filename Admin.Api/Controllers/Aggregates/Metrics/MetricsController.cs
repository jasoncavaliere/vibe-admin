
using Microsoft.AspNetCore.Mvc;
using Admin.Api.Models;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Placeholder: return some sample metrics
            var metrics = new {
                Uptime = "24h",
                Requests = 1234,
                Status = "Healthy"
            };
            return Ok(metrics);
        }

        [HttpGet("list-by-tenant")]
        public IActionResult ListByTenant([FromQuery] string sourceTenant, [FromQuery] string sourceSlot)
        {
            var result = new Medaitor().Send(new ListByTenantRequest { TenantId = sourceTenant });
            return Ok(result);
        }
    }
}
