namespace BlogEngine.Services.Policy
{
    using System.Runtime.CompilerServices;
    using Microsoft.AspNetCore.Authorization;

    public class RolePolicyRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; }

        public RolePolicyRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
