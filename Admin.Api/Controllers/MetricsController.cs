
using Microsoft.AspNetCore.Mvc;
using Admin.Api.Models;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet("list-by-tenant")]
        public async Task<IActionResult> ListByTenant([FromQuery] string sourceTenant, [FromQuery] string sourceSlot)
        {
            var request = new ListMeasuresByTenantRequest
            {
                SourceTenant = sourceTenant,
                SourceSlot = sourceSlot
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
