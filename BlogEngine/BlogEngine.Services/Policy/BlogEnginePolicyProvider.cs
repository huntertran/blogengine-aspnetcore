namespace BlogEngine.Services.Policy
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class BlogEnginePolicyProvider : IAuthorizationPolicyProvider
    {
        public async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
