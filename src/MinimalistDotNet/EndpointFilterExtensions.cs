using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MinimalistDotNet;

public static class EndpointFilterExtensions
{
    public static IEndpointConventionBuilder AddValidator<TParameter, TValidator>(this RouteHandlerBuilder builder) 
        where TValidator : AbstractValidator<TParameter>, new()
    {
        return builder.AddEndpointFilter<ParameterValidationPreProcessor<TParameter, TValidator>>();
    }

    public static IEndpointConventionBuilder AddExceptionHandler<TException>(this IEndpointConventionBuilder builder, Func<TException, object?> exceptionHandler)
        where TException : Exception
    {
        builder.AddEndpointFilter(new ExceptionPostProcessor<TException>(exceptionHandler));
        return builder;
    }
}