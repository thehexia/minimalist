using System;
using System.Net.NetworkInformation;
using ExampleApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalistDotNet;

namespace ExampleApi.Endpoints;

internal class GetCoffeePot : IEndpointGroup
{
    internal record class GetCoffeePotRequest(int CoffeePot);
    internal record class GetCoffeePotDto(string message);

    public Mapper Map => app => 
        app.MapGet("/api/coffeepot", ([FromBody] GetCoffeePotRequest request, IMyService myService) =>
        {
            if (request.CoffeePot == 418) throw new PingException("");
            if (request.CoffeePot == 419) throw new PathTooLongException();

            myService.DoThings();

            return Results.Ok<GetCoffeePotDto>(new("I'm a coffee pot."));
        })
        .AddValidator<GetCoffeePotRequest, CoffeePotValidator>()
        .AddExceptionHandler<PingException>((ex) => 
        {
            return Results.Problem("I'm a teapot.", statusCode: 418);
        })
        .AddExceptionHandler<PathTooLongException>((ex) => 
        {
            return Results.Problem("I'm stupid.", statusCode: 500);
        });

    internal class CoffeePotValidator : AbstractValidator<GetCoffeePotRequest>
    {
        public CoffeePotValidator()
        {
            RuleFor(x => x.CoffeePot).GreaterThan(0);
        }
    }
}
