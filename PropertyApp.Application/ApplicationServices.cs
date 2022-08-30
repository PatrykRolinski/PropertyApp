using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Services.CurrentUserService;
using PropertyApp.Application.Services.EmailService;
using PropertyApp.Application.Services.PhotoService;
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
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthorizationHandler, PropertyOperationRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, UserOperationRequirementHandler>();
        services.AddHttpContextAccessor();
        
        return services;
    }
}
