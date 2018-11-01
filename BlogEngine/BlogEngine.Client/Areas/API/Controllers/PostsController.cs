namespace BlogEngine.Client.Areas.API.Controllers
{
    using BlogEngine.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Generic;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdmin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireWriter")]
    public class PostsController : ControllerBase
    {
        private readonly IGenericRepository<Post> _repository;

        public PostsController(IGenericRepository<Post> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Post> All()
        {
            return _repository.Get();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Insert(post);
            await _repository.SaveChangesAsync();

            return Ok();
        }
    }
}