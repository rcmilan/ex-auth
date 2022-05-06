using App.CustomPolicyProvider;
using Microsoft.AspNetCore.Authorization;

namespace App.Authorization
{
    public class SecurityLevelHandler : AuthorizationHandler<SecurityLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SecurityLevelRequirement requirement)
        {
            var claimValue = Convert.ToInt32(context.User.Claims
                .FirstOrDefault(c => c.Type == DynamicPolicies.SecurityLevel)?
                .Value ?? "0");

            if (requirement.Level <= claimValue)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}