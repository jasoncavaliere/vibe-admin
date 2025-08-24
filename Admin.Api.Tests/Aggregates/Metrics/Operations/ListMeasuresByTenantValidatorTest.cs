using Xunit;
using Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;
using Admin.Api.Models;

namespace Admin.Api.Tests.Aggregates.Metrics.Operations;

public class ListMeasuresByTenantValidatorTest
{
    private readonly ListMeasuresByTenantValidator _validator = new ListMeasuresByTenantValidator();

    [Fact]
    public void ShouldReturnError_WhenSourceTenantIsNullOrEmpty()
    {
        var request = new ListMeasuresByTenantRequest
        {
            SourceTenant = null,
            SourceSlot = "slot1"
        };

        var messages = _validator.GetMessages(request);
        Assert.Equal(1, messages.Length);
        Assert.Contains(messages, m => m.State == OperationState.Error && m.Text.Contains("You must specify Tenant"));
    }

    [Fact]
    public void GetMessages_ShouldReturnError_WhenSourceSlotIsNullOrEmpty()
    {
        var request = new ListMeasuresByTenantRequest
        {
            SourceTenant = "tenant1",
            SourceSlot = ""
        };

        var messages = _validator.GetMessages(request);

        Assert.Contains(messages, m => m.State == OperationState.Error && m.Text.Contains("You must specify Slot"));
        Assert.Equal(1, messages.Length);
    }

    [Fact]
    public void GetMessages_ShouldReturnNoError_WhenSourceTenantAndSlotAreProvided()
    {
        var request = new ListMeasuresByTenantRequest
        {
            SourceTenant = "tenant1",
            SourceSlot = "slot1"
        };

        var messages = _validator.GetMessages(request);
        Assert.Equal(0, messages.Length);
    }
}