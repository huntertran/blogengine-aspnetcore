namespace BlogEngine.Client.Areas.API.Controllers
{
    using BlogEngine.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Identity;
    using System.Threading.Tasks;
    using ViewModels;

    [Route("api/[controller]/[action]")]
    [Area(nameof(API))]
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

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] AuthViewModel viewModel)
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