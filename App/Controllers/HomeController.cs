using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            var user = HttpContext.User;

            return View();
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
        public async Task<IActionResult> Login(string username, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if(identityUser != null)                
                return await SignIn(password, identityUser);

            return RedirectToAction("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var identityUser = new IdentityUser(username);

            var result = await _userManager.CreateAsync(identityUser, password);

            if(result.Succeeded)
                return await SignIn(password, identityUser);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> SignIn(string password, IdentityUser identityUser)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(identityUser, password, isPersistent: false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
            {
                // ações após success no sign in
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Authenticate()
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "UserName"),
        //        new Claim(ClaimTypes.Email, "user@email.com")
        //    };

        //    var hueClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "Hue br"),
        //        new Claim("hu3huehue", "brbrbr")
        //    };

        //    var identity = new ClaimsIdentity(claims, "identity123");
        //    var hueIdentity = new ClaimsIdentity(hueClaims, "huebr identity");

        //    var userPrincipal = new ClaimsPrincipal(new[] { identity, hueIdentity });

        //    HttpContext.SignInAsync(userPrincipal);

        //    return RedirectToAction("Index");
        //}
    }
}