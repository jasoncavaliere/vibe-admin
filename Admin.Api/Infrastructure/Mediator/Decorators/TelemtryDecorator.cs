namespace Admin.Api.Infrastructure.Mediator.Decorators;

using Admin.Api.Models;
using Microsoft.ApplicationInsights;
using System;
using System.Threading.Tasks;

public class TelemetryDecorator : IOperationDecorator<Decorators>
{
    public TelemetryClient TelemetryClient { get; }

    public TelemetryDecorator(TelemetryClient telemetryClient)
    {
        TelemetryClient = telemetryClient;
    }

    public async Task<OperationResult<TResponse>> Handle<TResponse>(IRequest<TResponse> request, Func<Task<OperationResult<TResponse>>> next)
    {
        // Start telemetry
        TelemetryClient.TrackEvent("OperationDecorator::Start", new System.Collections.Generic.Dictionary<string, string>
        {
            { "RequestType", request.GetType().Name }
        });
        var result = await next();
        // Stop telemetry
        TelemetryClient.TrackEvent("OperationDecorator::Complete", new System.Collections.Generic.Dictionary<string, string>
        {
            { "RequestType", request.GetType().Name }
        });
        return result;
    }
}