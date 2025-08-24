using Admin.Api.Models;
using Microsoft.ApplicationInsights;

public interface IOperationDecorator<Decorators>
{
    Task<OperationResult<TResponse>> Handle<TResponse>(IRequest<TResponse> request, Func<Task<OperationResult<TResponse>>> next);
}

public enum Decorators{
    Telemetry,
    Logging
}


public class DecoratorFactory{
    public static IList<IOperationDecorator<Decorators>> Build(IServiceProvider serviceProvider){
        return new List<IOperationDecorator<Decorators>>{
            new TelemetryDecorator(serviceProvider.GetRequiredService<TelemetryClient>()),
            new LoggingDecorator()
        };
    }
}