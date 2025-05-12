using Microsoft.AspNetCore.Mvc;

namespace InternetRustShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
