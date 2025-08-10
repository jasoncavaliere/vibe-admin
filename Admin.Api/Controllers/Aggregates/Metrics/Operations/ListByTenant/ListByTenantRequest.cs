public class ListByTenantRequest : IRequest<MeasureList>
{
    public string TenantId { get; set; } = "";
    public string SlotId { get; set; } = "";
}


    public class MeasureList
    {
        public IList<Measure> Measures { get; set; } = new List<Measure>();
        public string SourceTenant { get; set; } = "";
        public string SourceSlot { get; set; } = "";
    }


    public class Measure
    {
        public string? Name { get; set; }
        public double Value { get; set; }
        public string? Unit { get; set; }
    }

