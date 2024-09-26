using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace MinimalistDotNet;

public class ParameterValidationPreProcessor<TParameter, TValidator> : IEndpointFilter 
    where TValidator : AbstractValidator<TParameter>, new()
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var parameter = context.GetArgument<TParameter>(0);
        TValidator validator = new();
        ValidationResult validationResult = validator.Validate(parameter);
        
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult);
        }

        return await next(context);
    }
}