using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Services.EmailService;
using PropertyApp.Domain.Common;
using PropertyApp.Domain.Entities;
using System.Reflection;

namespace PropertyApp.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.Configure<EmailSettings>(config.GetSection("EmailService"));
        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}
