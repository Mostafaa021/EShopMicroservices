using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Services to Container

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
var app = builder.Build();

// Configure HTTP Request PipeLine

app.MapCarter();
app.Run();