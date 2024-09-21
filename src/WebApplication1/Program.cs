using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpClient ctx) =>
{
    return TypedResults.Ok(new
    {
        Hello = "World"
    });
}).AddEndpointFilter<EndpointFilter>();

app.Run();

