using ApplicationDomain;
using InternetRustShop.Models;
using Microsoft.AspNetCore.Mvc;
using UserBLL;

namespace InternetRustShop.Controllers
{
    public class MainPageController : Controller
    {
        private readonly IUserService _userService;

        public MainPageController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Розбан", Description = "Розбан для учасника", Cost = 500 },
                new Product { Id = 2, Name = "VIP", Description = "Vip", Cost = 1500 },
                new Product { Id = 3, Name = "Монітор", Description = "27\" IPS Full HD монітор", Cost = 4500 },
                new Product { Id = 4, Name = "Навушники", Description = "Ігрові навушники з мікрофоном", Cost = 1200 },
                new Product { Id = 5, Name = "Килимок для миші", Description = "Великий килимок з RGB-підсвіткою", Cost = 300 }
            };
            return View(products);
        }
    }
}
