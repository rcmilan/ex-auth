using App.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace App.CustomPolicyProvider
{
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            // options = realiza um override no services.AddAuthorization do program.cs
        }

        public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // policyName = {type}.{value}
            foreach (var customPolicy in DynamicPolicies.Get())
            {
                if (policyName.StartsWith(customPolicy))
                {
                    var policy = DynamicAuthorizationPolicyFactory.Create(policyName);

                    return Task.FromResult(policy);
                }
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}