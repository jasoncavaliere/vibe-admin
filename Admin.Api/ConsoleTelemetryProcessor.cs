using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

public class ConsoleTelemetryProcessor : ITelemetryProcessor
{
    private ITelemetryProcessor _next;

    public ConsoleTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        // Write event name and properties to the console for EventTelemetry
        if (item is Microsoft.ApplicationInsights.DataContracts.EventTelemetry evt)
        {
            Console.WriteLine($"[AI Event] {evt.Name}");
            foreach (var prop in evt.Properties)
            {
                Console.WriteLine($"  {prop.Key}: {prop.Value}");
            }
        }
        _next.Process(item);
    }
}
