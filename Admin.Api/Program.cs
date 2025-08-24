
using System.Reflection;
using Admin.Api.Models;
using Admin.Api.Infrastructure.Mediator;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Application Insights telemetry
builder.Services.AddApplicationInsightsTelemetry();
// Register ConsoleTelemetryProcessor for local console output


// Register Mediator
builder.Services.AddScoped<IMediator, Mediator>();

// Register all IRequestHandler implementations
var handlerInterface = typeof(IRequestHandler<,>);
var validatorInterface = typeof(IValidator<,>);
var assemblies = new[] { Assembly.GetExecutingAssembly() };
var types = assemblies.SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && !t.IsInterface);
var handlerTypes = types    
    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
    .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == handlerInterface)
    .ToList();



foreach (var handler in handlerTypes)
{
    builder.Services.AddScoped(handler.Interface, handler.Type);
}

var validatorTypes = types    
    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
    .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == validatorInterface)
    .ToList();

foreach (var validator in validatorTypes)
{
    builder.Services.AddScoped(validator.Interface, validator.Type);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
