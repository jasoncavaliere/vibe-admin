
using Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;

public class ListMeasuresByTenantRequest : IRequest<ListMeasuresByTenantResponse>
{
    public string SourceTenant { get; set; } = string.Empty;
    public string SourceSlot { get; set; } = string.Empty;
}

public class ListMeasuresByTenantResponse
{
    public string SourceTenant { get; set; } = string.Empty;
    public string SourceSlot { get; set; } = string.Empty;
    public List<Measure> Measures { get; set; } = new List<Measure>();
}


public class Measure
{
    public string Name { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
}