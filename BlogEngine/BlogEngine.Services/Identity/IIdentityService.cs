namespace BlogEngine.Services.Identity
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IIdentityService
    {
        Task<Dictionary<string, string>> GenerateToken(ApplicationUser user);
    }
}
