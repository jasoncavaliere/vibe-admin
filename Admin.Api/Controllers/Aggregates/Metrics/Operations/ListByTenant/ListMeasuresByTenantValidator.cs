using Admin.Api.Infrastructure.Mediator;
using Admin.Api.Models;
using System.Threading.Tasks;



public class ListMeasuresByTenantValidator : BaseValidator<ListMeasuresByTenantRequest, ListMeasuresByTenantResponse>
{ 
   public override  Message[] GetMessages(ListMeasuresByTenantRequest request){
        return new ValidationMessageBuilder()
            .WithNullEmpty(request.SourceTenant,OperationState.Error,"You must specify SourceTenant")
            .WithNullEmpty(request.SourceSlot,OperationState.Error,"You must specify SourceSlot")
        .Build();
   }
}