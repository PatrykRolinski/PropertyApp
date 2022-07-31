﻿using PropertyApp.Application.Exceptions;
using Microsoft.Extensions.Logging;

namespace PropertyApp.API.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch(ForbiddenException forbiddenException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbiddenException.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Internal server error. ", ex);
             context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.ToString());
        }
    }
}
