using Admin.Api.Infrastructure.Mediator;
using Admin.Api.Models;
using System.Threading.Tasks;

namespace Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;


public class ListMeasuresByTenantValidator : BaseValidator<ListMeasuresByTenantRequest, ListMeasuresByTenantResponse>
{ 
   public override  Message[] GetMessages(ListMeasuresByTenantRequest request){
        return new ValidationMessageBuilder()
            .WithNullEmpty(request.SourceTenant,OperationState.Error,"You must specify Tenant")
            .WithNullEmpty(request.SourceSlot,OperationState.Error,"You must specify Slot")
        .Build();
   }
}