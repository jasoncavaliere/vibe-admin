using Admin.Api.Models;

public class Medaitor : IMediator

{
    public Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request)
    {
        throw new NotImplementedException();
    }
}

public interface IMediator
{
    Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request);  
}

public interface IRequest<TResponse>
{
}