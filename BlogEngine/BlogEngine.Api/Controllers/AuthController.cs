namespace BlogEngine.Api.Controllers
{
    using Client.Areas.API.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Identity;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // find user
                var user = await _userManager.FindByNameAsync(viewModel.Username);

                if (user != null)
                {
                    var validPassword = await _userManager.CheckPasswordAsync(user, viewModel.Password);

                    if (validPassword)
                    {
                        var token = await _identityService.GenerateToken(user);
                        return Ok(token);
                    }
                }
            }

            return NotFound();
        }
    }
}