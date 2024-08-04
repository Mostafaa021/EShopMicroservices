
var builder = WebApplication.CreateBuilder(args);
#region Services Registering
// Configure and  Register Services
var assembly = typeof(Program).Assembly;

//builder.Services.AddMarten(options =>
//{
//    options.Connection(builder.Configuration.GetConnectionString("MartenDB")!);
//}).UseLightweightSessions();

//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.InitializeMartenWith<CatalogInitialData>();
//}
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    // Register PipelineBehaviour for MediatR of Type ValidationBehavior
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));

});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddHealthChecks() // AddHelthChecks for Catalog Microservice Only
//    .AddNpgSql(builder.Configuration.GetConnectionString("MartenDB")!); // AAddHelthChecks for Postgres SQL 

#endregion

var app = builder.Build();

//Configure HTTP Request PipeLine
#region Request Pipeline Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.MapCarter();
app.UseDeveloperExceptionPage();
app.UseExceptionHandler(opt => { });
//app.UseHealthChecks("/health",
//    new HealthCheckOptions()
//    {
//        ResponseWriter = UIres.WriteHealthCheckUIResponse
//    });

app.Run();
#endregion