using Microsoft.AspNetCore.Authorization;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Security.Claims;

namespace PropertyApp.Application.Authorization;

public class PropertyOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Property>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Property property)
    {
        if (requirement.ResourceOperation == ResourceOperation.Read)
        {
            context.Succeed(requirement);
        }
       
        var userId=  context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

        if ((requirement.ResourceOperation==ResourceOperation.Delete || requirement.ResourceOperation==ResourceOperation.Update) 
            && property.CreatedById == Guid.Parse(userId)|| role==RoleName.Admin.ToString())
        {
            context.Succeed(requirement);
        }

        if (requirement.ResourceOperation == ResourceOperation.Create && 
            (role == RoleName.Admin.ToString() || role == RoleName.Manager.ToString()))
        {
            context.Succeed(requirement);
        }


        return Task.CompletedTask;
    }
}
