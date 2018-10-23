namespace BlogEngine.Services.Policy
{
    using Microsoft.AspNetCore.Authorization;

    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private const string PolicyPrefix = "Role";

        public RoleAuthorizeAttribute(RoleName roleName)
        {
            Policy = $"{PolicyPrefix}{roleName}";
        }
    }
}
