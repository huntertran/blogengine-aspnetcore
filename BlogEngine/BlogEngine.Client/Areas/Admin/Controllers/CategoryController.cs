namespace BlogEngine.Client.Areas.Admin.Controllers
{
    using BlogEngine.Models;
    using Microsoft.AspNetCore.Mvc;
    using Repository.Generic;

    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var categories = _repository.Get();
            return View(categories);
        }
    }
}