
using Microsoft.AspNetCore.Mvc;
using Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
    private readonly IMediator _mediator;
    private readonly ILogger<MetricsController> _logger;

        public MetricsController(IMediator mediator, ILogger<MetricsController> logger, Microsoft.ApplicationInsights.TelemetryClient telemetryClient)
        {
            _mediator = mediator;
            _logger = logger;
        }

       
        [HttpGet("list-by-tenant")]
        public async Task<IActionResult> ListByTenant([FromQuery] string sourceTenant = "", [FromQuery] string sourceSlot = "")
        {
            _logger.LogInformation("ListByTenant called with SourceTenant: {SourceTenant}, SourceSlot: {SourceSlot}", sourceTenant, sourceSlot);
            var result = await _mediator.Send(new ListMeasuresByTenantRequest
            {
                SourceTenant = sourceTenant,
                SourceSlot = sourceSlot
            });
            return Ok(result);
        }
    }
}
