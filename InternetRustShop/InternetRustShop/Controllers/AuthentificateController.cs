using ApplicationDomain;
using InternetRustShop.SessionModel;
using Microsoft.AspNetCore.Mvc;
using UserBLL;

namespace InternetRustShop.Controllers
{
    public class AuthentificateController : Controller
    {
        private readonly IUserService _userService;

        public AuthentificateController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Name, string Password)
        {
            var user = new User
            {
                Name = Name,
                Password = Password
            };
            if (await _userService.LoginUser(user) == false)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    HttpOnly = true
                };

                string name = user.Name;
                string password = user.Password;

                var userFromDB = (await _userService.GetByConditionAsync(x => x.Name == name && x.Password == password))
                                 .FirstOrDefault();

                if (userFromDB != null)
                {
                    string userIdStr = userFromDB.Id.ToString();
                    Response.Cookies.Append("UserId", userIdStr, cookieOptions);
                    SessionStore.OnlieUsers[userIdStr] = new User
                    {
                        Id = userFromDB.Id,
                        Name = userFromDB.Name,
                        Email = userFromDB.Email,
                        Password = userFromDB.Password
                    };
                }

                return RedirectToAction("Index", "MainPage");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (await _userService.RegisterUser(user))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    HttpOnly = true
                };

                string name = user.Name;
                string password = user.Password;

                var userFromDB = (await _userService.GetByConditionAsync(x => x.Name == name && x.Password == password))
                                 .FirstOrDefault();

                if (userFromDB != null)
                {
                    string userIdStr = userFromDB.Id.ToString();
                    Response.Cookies.Append("UserId", userIdStr, cookieOptions);
                    SessionStore.OnlieUsers[userIdStr] = new User
                    {
                        Id = userFromDB.Id,
                        Name = userFromDB.Name,
                        Email = userFromDB.Email,
                        Password = userFromDB.Password
                    };

                    return RedirectToAction("Index", "MainPage");
                }
            }

            return View();
        }

        public IActionResult LogOut()
        {
            var cookie = Request.Cookies["UserId"];
            if (!string.IsNullOrEmpty(cookie))
            {
                SessionStore.OnlieUsers.Remove(cookie);
                Response.Cookies.Delete("UserId");
            }

            return RedirectToAction("Index");
        }
    }
}
