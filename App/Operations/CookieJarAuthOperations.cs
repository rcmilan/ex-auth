using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace App.Operations
{
    public static class CookieJarAuthOperations
    {
        public static OperationAuthorizationRequirement Open = new()
        {
            Name = CookieJarOperations.Open
        };
    }
}