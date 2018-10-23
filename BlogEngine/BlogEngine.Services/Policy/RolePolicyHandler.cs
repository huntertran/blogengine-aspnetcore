namespace BlogEngine.Services.Policy
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    public class RolePolicyHandler : AuthorizationHandler<RolePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RolePolicyRequirement requirement)
        {
            if (context.User.IsInRole(requirement.RoleName))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
