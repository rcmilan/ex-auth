using App.Operations;
using App.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Open()
        {
            var resource = new CookieJar(); // ex: buscar resource do db

            await _authorizationService.AuthorizeAsync(User, resource, CookieJarAuthOperations.Open);

            return View("Index");
        }
    }
}