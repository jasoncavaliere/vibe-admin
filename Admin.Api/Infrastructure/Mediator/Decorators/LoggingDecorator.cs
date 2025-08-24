namespace Admin.Api.Infrastructure.Mediator.Decorators;

using Admin.Api.Models;
using System;
using System.Threading.Tasks;

public class LoggingDecorator : IOperationDecorator<Decorators>
{
    public async Task<OperationResult<TResponse>> Handle<TResponse>(IRequest<TResponse> request, Func<Task<OperationResult<TResponse>>> next)
    {
        Console.WriteLine($"[LOG] Operation Start | RequestType: {request.GetType().Name} | HandlerType: {typeof(TResponse).Name}");
        try
        {
            var result = await next();
            Console.WriteLine($"[LOG] Operation Complete | RequestType: {request.GetType().Name} | HandlerType: {typeof(TResponse).Name}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
}
       