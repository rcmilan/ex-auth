using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace App.Transformer
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        { // proxy sempre que o usuário é autenticado
            var hasClaim123 = principal.Claims.Any(c => c.Type == "CookieJarClaim1");

            if (!hasClaim123)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("CookieJarClaim1", "CookieJarValue1"));
            }

            return Task.FromResult(principal);
        }
    }
}