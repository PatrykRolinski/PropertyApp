using Microsoft.AspNetCore.Authorization;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Security.Claims;

namespace PropertyApp.Application.Authorization;

public class UserOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, User>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, User user)
    {
        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

        if ((requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Delete || requirement.ResourceOperation == ResourceOperation.Update)
           && (user.Id == Guid.Parse(userId) || role == RoleName.Admin.ToString())) 
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
