using Microsoft.AspNetCore.Http;
using PropertyApp.Application.Contracts.IServices;
using System.Security.Claims;

namespace PropertyApp.Application.Services.CurrentUserService;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                 
    public string? UserRole => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
}                
