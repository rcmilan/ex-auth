using Microsoft.AspNetCore.Authorization;

namespace App.Authorization
{
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        public CustomRequireClaimHandler()
        {
            // tem acesso ao DI container
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
        {
            if (HasClaim(context, requirement))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private static bool HasClaim(AuthorizationHandlerContext context, CustomRequireClaim requirement)
        {
            return context.User.Claims.Any(c => c.Type == requirement.ClaimType);
        }
    }
}