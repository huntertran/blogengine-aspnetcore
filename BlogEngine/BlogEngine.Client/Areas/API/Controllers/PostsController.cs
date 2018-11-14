namespace BlogEngine.Client.Areas.API.Controllers
{
    using System;
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

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _repository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.CreatedDateTime = DateTime.UtcNow;
            post.EditedDateTime = DateTime.UtcNow;
            post.PostedDateTime = DateTime.UtcNow;

            _repository.Insert(post);
            await _repository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.EditedDateTime = DateTime.UtcNow;
            post.PostedDateTime = DateTime.UtcNow;

            _repository.Update(post);
            await _repository.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _repository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();

            return Ok(post);
        }
    }
}