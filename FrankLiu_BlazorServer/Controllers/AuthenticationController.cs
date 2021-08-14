using FrankLiu_BlazorServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrankLiu_BlazorServer.Controllers
{
    [Route("/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public async Task<ActionResult> Login([FromQuery] string username, [FromQuery] string pwd, [FromQuery] bool persistCookie)
        {
            if (!ModelState.IsValid)
                return Redirect("/loginPage");

            if (username == "admin" && pwd == "pwd")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Vieriu Alexandru"),
                    new Claim(ClaimTypes.Email, "HR@gmail.com"),
                    new Claim(ClaimTypes.Role, "HR"),
                    //new Claim("Manager", "HRManager"),
                    new Claim("Sucursala", "GARSP"),
                    new Claim("DataNastere", "1989-07-07")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = persistCookie
                };

                //await HttpContext.SignInAsync("CookieScheme", claimsPrincipal, authProperties);
                
                await HttpContext.SignInAsync("CookieScheme", claimsPrincipal);

                return Redirect("/");
            }

            return Redirect("/loginPage");
        }


        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}
