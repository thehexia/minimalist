using System.Text.Json.Serialization;
using ExampleApi.Endpoints;
using ExampleApi.Services;
using MinimalistDotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddMinimalistEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseMinimalistEndpoints();

app.Run();