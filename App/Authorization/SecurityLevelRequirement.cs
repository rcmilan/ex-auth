using Microsoft.AspNetCore.Authorization;

namespace App.Authorization
{
    public class SecurityLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; }

        public SecurityLevelRequirement(int level)
        {
            Level = level;
        }
    }
}