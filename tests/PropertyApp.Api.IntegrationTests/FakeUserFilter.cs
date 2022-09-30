using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PropertyApp.Api.IntegrationTests
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrinicpal = new ClaimsPrincipal(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "53bfba37-1b94-41e3-abfa-bbc7c8cc5ae9"),
                    new Claim(ClaimTypes.Role, "Admin")
                }));

            context.HttpContext.User = claimsPrinicpal;

            await next();
        }
    }
}
