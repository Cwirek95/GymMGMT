using GymMGMT.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GymMGMT.Application.Security.Authorization
{
    public class TrainingOwnerRequirementHandler : AuthorizationHandler<TrainingOwnerRequirement, Training>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TrainingOwnerRequirement requirement, Training resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(resource.CreatedBy.Equals(userId, StringComparison.OrdinalIgnoreCase))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}