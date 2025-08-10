using Admin.Api.Models;


public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
            throw new InvalidOperationException($"Handler for {request.GetType().Name} not registered.");
        return await handler.Handle((dynamic)request);
    }
}

public interface IMediator
{
    Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request);
}

public interface IRequest<TResponse>
{
}

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<OperationResult<TResponse>> Handle(TRequest request);
}