namespace BlogEngine.Client.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area(nameof(Admin))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}