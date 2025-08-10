using Admin.Api.Models;

namespace Admin.Api.Infrastructure.Mediator
{
    public interface IValidator<TRequest,TResponse>
    {
        Task<OperationResult<TResponse>> Validate(TRequest request);
    }
}
