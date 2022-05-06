using App.CustomPolicyProvider;
using Microsoft.AspNetCore.Authorization;

namespace App.Attributes
{
    public class SecurityLevelAttribute : AuthorizeAttribute
    {
        public SecurityLevelAttribute(int level)
        {
            Policy = $"{DynamicPolicies.SecurityLevel}.{level}";
        }
    }
}