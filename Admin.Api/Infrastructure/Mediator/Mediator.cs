
using Admin.Api.Models;
using Admin.Api.Infrastructure.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void writeTelemtry(Microsoft.ApplicationInsights.TelemetryClient client, String operation, String requestType, String handlerType){
         
        client.TrackEvent( operation, new Dictionary<string, string>{
            { "RequestType", requestType },
            { "HandlerType", handlerType }
        });
    }


    public async Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var validatorType = typeof(IValidator<,>).MakeGenericType(requestType, typeof(TResponse));
        dynamic? validator = _serviceProvider.GetService(validatorType);
        var client = _serviceProvider.GetService<Microsoft.ApplicationInsights.TelemetryClient>();
       writeTelemtry(client,"OperationValidation::Start", requestType.Name,handlerType.Name);
        if (validator != null)
        {
            var vResult = await validator.Validate((dynamic)request);
            if(vResult.State == OperationState.Error || vResult.State==OperationState.Exception)
            {
                writeTelemtry(client,"OperationValidation::Failure", requestType.Name,handlerType.Name);
                return vResult;
            }
        }
        writeTelemtry(client,"OperationValidation::Success", requestType.Name,handlerType.Name);
        writeTelemtry(client,"OperationHandler::Start", requestType.Name,handlerType.Name);
        
        dynamic? handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {                

            writeTelemtry(client,"OperationHandler::Fail", requestType.Name,handlerType.Name);
            throw new InvalidOperationException($"Handler for {requestType.Name} not registered.");
        }

        var result = await handler.Handle((dynamic)request); 
        writeTelemtry(client,"OperationHandler::Complete", requestType.Name,handlerType.Name);
        return result;
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