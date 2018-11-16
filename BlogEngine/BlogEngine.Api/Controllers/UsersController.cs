namespace BlogEngine.Api.Controllers
{
    using Client.Areas.API.ViewModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            var users = _userManager.Users.Select(x => new UserViewModel(x));

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(x => x.Name);

            var vm = new UserViewModel(user, currentRoles, allRoles);

            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            var rolesToAdd = vm.Roles.Where(x => x.IsInRole).Select(x => x.RoleName);
            var rolesToRemove = vm.Roles.Where(x => !x.IsInRole && x.RoleName != "Admin").Select(x => x.RoleName);

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}