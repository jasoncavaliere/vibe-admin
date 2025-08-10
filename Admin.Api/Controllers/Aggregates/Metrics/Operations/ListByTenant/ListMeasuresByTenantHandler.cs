using Admin.Api.Models;

namespace Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;

public class ListMeasuresByTenantHandler : BaseHandler<ListMeasuresByTenantRequest, ListMeasuresByTenantResponse>
{
    public override Task<OperationResult<ListMeasuresByTenantResponse>> Handle(ListMeasuresByTenantRequest request)
    {
             // Example: return a static list for demonstration
        var result = new ListMeasuresByTenantResponse();
        result.SourceSlot = request.SourceSlot;
        result.SourceTenant = request.SourceTenant;
        result.Measures = new List<Measure>
        {
            new Measure { Name = "CPU", Value = 0.75, Unit = "%" },
            new Measure { Name = "Memory", Value = 2048, Unit = "MB" },
            new Measure { Name = "Disk", Value = 120, Unit = "GB" }
        };

        return Success(result);
    }
}
