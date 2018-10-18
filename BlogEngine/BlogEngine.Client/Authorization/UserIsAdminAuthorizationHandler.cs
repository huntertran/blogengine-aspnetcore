namespace BlogEngine.Client.Authorization
{
    using System.Threading.Tasks;
    using BlogEngine.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Repository.Data;

    //public class UserIsAdminAuthorizationHandler 
    //    : AuthorizationHandler<OperationAuthorizationRequirement, ApplicationUser>
    //{
    //    private UserManager<ApplicationUser> _userManager;

    //    public UserIsAdminAuthorizationHandler(UserManager<ApplicationUser> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    protected override Task HandleRequirementAsync(
    //        AuthorizationHandlerContext context,
    //        OperationAuthorizationRequirement requirement,
    //        ApplicationUser resource)
    //    {
    //        if (context.User == null || resource == null)
    //        {
    //            return Task.CompletedTask;
    //        }

    //        if (requirement.Name != Constants.CreateOperationName &&
    //            requirement.Name != Constants.ReadOperationName &&
    //            requirement.Name != Constants.UpdateOperationName &&
    //            requirement.Name != Constants.DeleteOperationName)
    //        {
    //            return Task.CompletedTask;
    //        }

    //        if (resource.OwnerID == _userManager.GetUserId(context.User))
    //        {
    //            context.Succeed(requirement);
    //        }

    //        return Task.CompletedTask;
    //    }
    //}
}
