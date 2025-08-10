
using Admin.Api.Models;
using Admin.Api.Infrastructure.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var validatorType = typeof(IValidator<,>).MakeGenericType(requestType, typeof(TResponse)    );
        dynamic? validator = _serviceProvider.GetService(validatorType);

            Console.WriteLine($"Validating request of type {requestType.Name}");
        if (validator != null)
        {
            var result = await validator.Validate((dynamic)request);
            if(result.State == OperationState.Error || result.State==OperationState.Exception)
            {
                return result;
            }
        }


        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
            throw new InvalidOperationException($"Handler for {requestType.Name} not registered.");
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