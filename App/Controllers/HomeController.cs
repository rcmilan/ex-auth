﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IAuthorizationService _authorizationService;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> HelloWorld()
        {
            var customPolicy = new AuthorizationPolicyBuilder("Schema")
                .RequireClaim("Hello")
                .Build();

            var authResult = await _authorizationService.AuthorizeAsync(User, customPolicy);

            if (authResult.Succeeded)
                Console.WriteLine("Hello World");

            return View("Index");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if (identityUser != null)
                return await SignIn(password, identityUser);

            return RedirectToAction("Register");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var identityUser = new IdentityUser(username);

            var result = await _userManager.CreateAsync(identityUser, password);

            if (result.Succeeded)
                return await SignIn(password, identityUser);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Secret()
        {
            var user = HttpContext.User;

            return View();
        }

        [Authorize(Policy = "Claim.Hu3")]
        public IActionResult SecretPolicy()
        {
            var user = HttpContext.User;

            return RedirectToAction("Secret");
        }

        private async Task<IActionResult> SignIn(string password, IdentityUser identityUser)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(identityUser, password, isPersistent: false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
            {
                await _signInManager.SignInWithClaimsAsync(identityUser, isPersistent: false, new[] { new Claim("hue.claim", "hue hue hu3 brbr") });
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