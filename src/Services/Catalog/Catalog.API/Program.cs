using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add Services to Container

builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("MartenDB")!);
}).UseLightweightSessions();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
var app = builder.Build();

// Configure HTTP Request PipeLine

app.MapCarter();
app.Run();
