using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication1;


public interface IService<TParam, TResult>
{
    ValueTask<TResult> ExecuteAsync(TParam query);
}


public class EndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var ret = await next(context);

        return ret switch
        {
            IValueHttpResult _ => Results.Ok(),
            _ => ret switch
            {
                Ok => ret,
                _ => ret
            }
        };
    }
}
