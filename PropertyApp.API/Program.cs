using NLog;
using NLog.Web;
using PropertyApp.API.Middleware;
using PropertyApp.Application;
using PropertyApp.Infrastructure;
using System.Text.Json.Serialization;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
var builder = WebApplication.CreateBuilder(args);


    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPropertyAppInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();



var app = builder.Build();

var scope = app.Services.CreateScope();
var dummySeeder = scope.ServiceProvider.GetService<SeedData>();

// Configure the HTTP request pipeline.
dummySeeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

   
app.Run();
    
}
catch (Exception exception)
{
    logger.Error(exception, "Exception occured");
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
