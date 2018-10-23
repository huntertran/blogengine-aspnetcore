namespace BlogEngine.Services.Policy
{
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
