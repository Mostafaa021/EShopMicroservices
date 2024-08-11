
var builder = WebApplication.CreateBuilder(args);
#region Services Registering
// Configure and  Register Service

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//using Scrutor Library to implment Decorator Design Pattern
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
//Register Redis Service 
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});

#region Adding Proxy Pattern for CachedRepo Manually but not scalable and manageable
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var bsaketrepo = provider.GetRequiredService<BasketRepository>(); 
//    return new CachedBasketRepository(bsaketrepo , provider.GetRequiredService<IDistributedCache>());
//});
#endregion

var assembly = typeof(Program).Assembly;

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("MartenDB")!);
    options.Schema.For<ShoppingCart>().Identity(x=>x.UserName); // Option in Marten Library to override default identifier of Entity
}).UseLightweightSessions();

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
builder.Services.AddHealthChecks() // AddHelthChecks for Basket Microservice Only
    .AddNpgSql(builder.Configuration.GetConnectionString("MartenDB")!) // Add Health Checks for Postgres SQL 
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!); //  Add Health Checks for Redis
#endregion

var app = builder.Build();

//Configure HTTP Request PipeLine
#region Request Pipeline Middlewares

app.MapCarter();
app.UseDeveloperExceptionPage();
app.UseExceptionHandler(opt => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
#endregion