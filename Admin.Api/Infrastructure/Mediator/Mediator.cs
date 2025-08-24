
    using Admin.Api.Models;
    using Admin.Api.Infrastructure.Mediator.Decorators;

namespace Admin.Api.Infrastructure.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<OperationResult<TResponse>> Send<TResponse>(IRequest<TResponse> request)
        {
            var decorators = DecoratorFactory.Build(_serviceProvider);
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            var validatorType = typeof(IValidator<,>).MakeGenericType(requestType, typeof(TResponse));
            dynamic? validator = _serviceProvider.GetService(validatorType);
            var client = _serviceProvider.GetService<Microsoft.ApplicationInsights.TelemetryClient>();

            // Validation pipeline
            Func<Task<OperationResult<TResponse>>> pipeline = async () => {
                if (validator != null)
                {
                    var vResult = await validator.Validate((dynamic)request);
                    if(vResult.State == OperationState.Error || vResult.State==OperationState.Exception)
                    {
                        return vResult;
                    }
                }
                dynamic? handler = _serviceProvider.GetService(handlerType);
                if (handler is null)
                {
                    throw new InvalidOperationException($"Handler for {requestType.Name} not registered.");
                }
                return await handler.Handle((dynamic)request);
            };

            // Apply decorators in order
            foreach (var decorator in decorators)
            {
                var next = pipeline;
                pipeline = () => decorator.Handle<TResponse>(request, next);
            }

            var result = await pipeline();
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
}
