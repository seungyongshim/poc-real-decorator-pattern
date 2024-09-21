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

        if(ret is IValueHttpResult v)
        {
            
        }

        return ret switch
        {
            Ok => ret,
            _ => ret
        };
    }
}
