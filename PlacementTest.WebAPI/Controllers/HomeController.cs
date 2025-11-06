using Microsoft.AspNetCore.Mvc;

namespace PlacementTest.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
