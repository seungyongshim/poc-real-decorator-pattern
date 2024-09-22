using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WebApplication1;

var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Ping).Assembly);
});

builder.Services.AddOpenTelemetry().WithTracing(tcb =>
{
    tcb.AddSource(serviceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .AddAspNetCoreInstrumentation()
    .AddConsoleExporter();
});

var app = builder.Build();


app.MapGet("/", (HttpContext ctx) =>
{
    return TypedResults.Ok(new
    {
        Hello = "World"
    });
}).AddEndpointFilter<EndpointFilter>();


app.MapGet("/ping", async ([FromServices] IMediator ctx) =>
{
    var result = await ctx.Send(new Ping());
    return TypedResults.Ok(result);
});

app.Run();
