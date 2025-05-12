using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using UserBLL;

namespace Authorize_Example_Custom_.Filters
{
    public class MyAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IUserService _userService;
        public MyAuthorizeFilter(IUserService userService)
        {
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookie = context.HttpContext.Request.Cookies["UserId"];
            if (cookie == null)
            {
                context.Result = new RedirectToActionResult("Login", "Authentificate", null);
            }
            else
            {
                var userId = int.Parse(cookie);
                if (userId == 0)
                {
                    context.Result = new RedirectToActionResult("Login", "Authentificate", null);
                }
                else
                {
                    var user = _userService.GetByIdAsync(userId).Result;
                    if (user == null)
                    {
                        context.Result = new RedirectToActionResult("Login", "Authentificate", null);
                    }
                    else if (user.Role == ApplicationDomain.Role.Admin || user.Role == ApplicationDomain.Role.SuperAdmin)
                    {
                        context.Result = new RedirectToActionResult("Index", "MainPage", new { area = "Admin" });
                    }
                }
            }
        }


    }
    
    
}
