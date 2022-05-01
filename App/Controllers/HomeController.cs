using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "UserName"),
                new Claim(ClaimTypes.Email, "user@email.com")                
            };

            var hueClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Hue br"),
                new Claim("hu3huehue", "brbrbr")
            };

            var identity = new ClaimsIdentity(claims, "identity123");
            var hueIdentity = new ClaimsIdentity(hueClaims, "huebr identity");

            var userPrincipal = new ClaimsPrincipal(new[] { identity, hueIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}