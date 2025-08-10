
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System;
using Admin.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Mediator
builder.Services.AddScoped<IMediator, Mediator>();

// Register all IRequestHandler implementations
var handlerInterface = typeof(IRequestHandler<,>);
var assemblies = new[] { Assembly.GetExecutingAssembly() };
var handlerTypes = assemblies
    .SelectMany(a => a.GetTypes())
    .Where(t => !t.IsAbstract && !t.IsInterface)
    .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
    .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == handlerInterface)
    .ToList();
foreach (var handler in handlerTypes)
{
    builder.Services.AddScoped(handler.Interface, handler.Type);
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
