using System.Threading.Tasks;
using System.Collections.Generic;
using Admin.Api.Models;

namespace Admin.Api.Models
{
    public class ListMeasuresByTenantRequest : IRequest<List<Measure>>
    {
        public string SourceTenant { get; set; } = string.Empty;
        public string SourceSlot { get; set; } = string.Empty;
    }

    public class ListMeasuresByTenantHandler : IRequestHandler<ListMeasuresByTenantRequest, List<Measure>>
    {
        public Task<OperationResult<List<Measure>>> Handle(ListMeasuresByTenantRequest request)
        {
            // Example: return a static list for demonstration
            var measures = new List<Measure>
            {
                new Measure { Name = "CPU", Value = 0.75, Unit = "%" },
                new Measure { Name = "Memory", Value = 2048, Unit = "MB" },
                new Measure { Name = "Disk", Value = 120, Unit = "GB" }
            };
            return Task.FromResult(OperationResult<List<Measure>>.Success(measures));
        }
    }
}
