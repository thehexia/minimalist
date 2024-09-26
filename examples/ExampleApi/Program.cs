using System.Text.Json.Serialization;
using ExampleApi.Endpoints;
using MinimalistDotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalistEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseMinimalistEndpoints();

app.Run();