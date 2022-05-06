using App.CustomPolicyProvider;
using Microsoft.AspNetCore.Authorization;

namespace App.Factories
{
    public static class DynamicAuthorizationPolicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            // policyName = {type}.{value}
            var parts = policyName.Split(".");

            var type = parts[0];
            var value = parts[1];

            switch (type)
            {
                case DynamicPolicies.Rank:
                    return new AuthorizationPolicyBuilder()
                        .RequireClaim(DynamicPolicies.Rank, value)
                        .Build();

                case DynamicPolicies.SecurityLevel:
                    return new AuthorizationPolicyBuilder()
                        .Build();

                default:
                    return null;
            }
        }
    }
}