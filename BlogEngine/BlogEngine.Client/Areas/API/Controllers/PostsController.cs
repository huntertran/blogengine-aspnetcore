namespace BlogEngine.Client.Areas.API.Controllers
{
    using BlogEngine.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Generic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels;

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdmin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireWriter")]
    public class PostsController : ControllerBase
    {
        private readonly IGenericRepository<Post> _repository;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<PostCategory> _postCategoryRepo;

        public PostsController(
            IGenericRepository<Post> repository,
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<PostCategory> postCategoryRepo)
        {
            _repository = repository;
            _categoryRepo = categoryRepo;
            _postCategoryRepo = postCategoryRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Post> All(int page = 1, int postPerPage = 5)
        {
            return _repository.GetByPage(
                page,
                postPerPage,
                orderBy: posts => posts.OrderByDescending(x => x.PostedDateTime));
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Post> GetPostsByCategory(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0)
        {
            if (categoryId == 0)
            {
                return All(page, postPerPage);
            }

            return _repository.GetByPage(
                page,
                postPerPage,
                filter: post => post.IsPublished == true,
                orderBy: posts => posts.OrderByDescending(x => x.PostedDateTime));
        }

        [HttpGet]
        [AllowAnonymous]
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

        [HttpGet]
        public async Task<IActionResult> GetCategoryOfPost([FromQuery] int postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new List<CategoryViewModel>();
            var categories = await _categoryRepo.GetAsync();
            var postCategoryList = await _postCategoryRepo.GetAsync(x => x.PostId == postId);

            foreach (var category in categories)
            {
                var postCategory = new CategoryViewModel(category);

                if (postCategoryList.Any(x => x.CategoryId == category.Id))
                {
                    postCategory.IsSelected = true;
                }

                result.Add(postCategory);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryOfPost(
            [FromBody] List<CategoryViewModel> categories,
            [FromQuery] int postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryToInsert = categories.Where(x => x.IsSelected).ToList();

            // Get old data
            var postCategoryList = await _postCategoryRepo.GetAsync(x => x.PostId == postId);

            // Remove old data
            _postCategoryRepo.DeleteRange(postCategoryList);

            // Create new list to insert
            var newListToInsert = new List<PostCategory>();

            foreach (var categoryVm in categoryToInsert)
            {
                var postCategory = new PostCategory
                {
                    CategoryId = categoryVm.Id,
                    PostId = postId
                };

                newListToInsert.Add(postCategory);
            }

            await _postCategoryRepo.InsertRangeAsync(newListToInsert);

            await _postCategoryRepo.SaveChangesAsync();

            return Ok();
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