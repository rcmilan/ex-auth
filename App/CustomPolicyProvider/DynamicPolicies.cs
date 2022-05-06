namespace App.CustomPolicyProvider
{
    // {type} do policyName
    public static class DynamicPolicies
    {
        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }

        public const string SecurityLevel = "SecutiryLevel";
        public const string Rank = "Rank";
    }
}