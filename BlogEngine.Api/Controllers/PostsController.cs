namespace BlogEngine.Api.Controllers
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repository.Generic;
    using Services.Post;
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
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<PostCategory> _postCategoryRepo;
        private readonly IPostService _postService;

        public PostsController(
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<PostCategory> postCategoryRepo,
            IPostService postService)
        {
            _categoryRepo = categoryRepo;
            _postCategoryRepo = postCategoryRepo;
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetTotalPostNumber()
        {
            return Ok(_postService.Count());
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<MinifiedPostViewModel> All(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0)
        {
            return _postService
                    .All(page, postPerPage, categoryId)
                    .Select(x => new MinifiedPostViewModel(x));
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<MinifiedPostViewModel> GetUnpublishedPosts(
            int page = 1,
            int postPerPage = 5,
            int categoryId = 0)
        {
            return _postService
                .GetUnpublishedPosts(page, postPerPage, categoryId)
                .Select(x => new MinifiedPostViewModel(x));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Find([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _postService.GetById(id);

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
        public IActionResult Post([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _postService.Insert(post);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _postService.Update(post);
            return Ok(post);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            _postService.Delete(id);

            return Ok(post);
        }
    }
}