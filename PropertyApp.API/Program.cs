using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using PropertyApp.API.Middleware;
using PropertyApp.Application;
using PropertyApp.Domain.Common;
using PropertyApp.Infrastructure;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

//[assembly: InternalsVisibleTo("PropertyApp.Api.IntegrationTests")]

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
var builder = WebApplication.CreateBuilder(args);


    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Setup JWT Authentication
    // TODO: Change for IOptions?"
    var authenticationSettings = new AuthenticationSetting();
    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
        option.DefaultScheme = "Bearer";
    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            
        };
    });


    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPropertyAppInfrastructureAsync(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(policy => policy
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").GetChildren().ToArray().Select(c => c.Value).ToArray()));
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
public partial class Program { }
