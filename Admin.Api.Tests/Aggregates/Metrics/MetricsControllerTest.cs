using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Admin.Api.Controllers;
using Admin.Api.Infrastructure.Mediator;
using Admin.Api.Controllers.Aggregates.Metrics.Operations.ListByTenant;
using Admin.Api.Models;

namespace Admin.Api.Tests.Controllers.Aggregates.Metrics
{
    public class MetricsControllerTest
    {
        public class ListByTenantTests
        {
            private readonly Mock<IMediator> _mediatorMock;
            private readonly Mock<ILogger<MetricsController>> _loggerMock;
            private readonly MetricsController _controller;

            public ListByTenantTests()
            {
                _mediatorMock = new Mock<IMediator>();
                _loggerMock = new Mock<ILogger<MetricsController>>();
                _controller = new MetricsController(_mediatorMock.Object, _loggerMock.Object);
            }

            [Fact]
            public async Task ListByTenant_ReturnsOkResult_WithExpectedData()
            {
                // Arrange
                var expectedResponse = new ListMeasuresByTenantResponse();
                var expectedResult = OperationResult<ListMeasuresByTenantResponse>.Success(expectedResponse);
                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<ListMeasuresByTenantRequest>()))
                    .ReturnsAsync(expectedResult);

                // Act
                var result = await _controller.ListByTenant("tenant1", "slotA");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(expectedResult, okResult.Value);
                _loggerMock.Verify(
                    l => l.Log(
                        LogLevel.Information,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("ListByTenant called with SourceTenant")),
                        null,
                        It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                    Times.Once);
            }

            [Fact]
            public async Task ListByTenant_UsesDefaultParameters_WhenNoneProvided()
            {
                // Arrange
                var expectedResponse = new ListMeasuresByTenantResponse();
                var expectedResult = OperationResult<ListMeasuresByTenantResponse>.Success(expectedResponse);
                _mediatorMock
                    .Setup(m => m.Send(It.IsAny<ListMeasuresByTenantRequest>()))
                    .ReturnsAsync(expectedResult);

                // Act
                var result = await _controller.ListByTenant();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(expectedResult, okResult.Value);
                _mediatorMock.Verify(m =>
                    m.Send(It.Is<ListMeasuresByTenantRequest>(r =>
                        r.SourceTenant == "" && r.SourceSlot == "")),
                    Times.Once);
            }
        }
    }
}