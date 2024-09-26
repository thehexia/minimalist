using System;
using Microsoft.AspNetCore.Http;

namespace MinimalistDotNet;

public class ExceptionPostProcessor<TException>(Func<TException, object?> exceptionHandler) : IEndpointFilter
    where TException : Exception
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try 
        {
            return await next(context);
        }
        catch (TException ex)
        {
            return exceptionHandler(ex);
        }
    }
}