namespace Admin.Api.Models
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<OperationResult<TResponse>> Handle(TRequest request);


    internal async Task<OperationResult<TResponse>> Success(TResponse result)
    {
        return await Task.FromResult(OperationResult<TResponse>.Success(result));
    }
}
}


