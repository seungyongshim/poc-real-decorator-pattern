using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication1;





public class EndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var ret = await next(context);

        if (ret is IValueHttpResult v )
        {
            var node = JsonSerializer.SerializeToNode(v.Value)!;

            if (node is JsonObject vv)
            {
                vv["traceId"] = Activity.Current?.Id;
            }

            return Results.Text(node.ToJsonString(), "application/json", statusCode: ((IStatusCodeHttpResult)ret).StatusCode);
        }

        return ret switch
        {
            
            _ => ret switch
            {
                Ok => ret,
                _ => ret
            }
        };
    }
}
