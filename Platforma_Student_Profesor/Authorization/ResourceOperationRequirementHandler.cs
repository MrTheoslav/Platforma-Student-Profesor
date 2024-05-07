using DAL;
using Microsoft.AspNetCore.Authorization;
using MODEL.Models;
using System.Security.Claims;

namespace API.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Repository>
    {
     
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Repository repository)
        {
            if(requirement.ResourceOperation==ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create
                )
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(repository.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
