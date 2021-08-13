using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrankLiu_BlazorServer.Controllers
{
    [Route("/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public async Task<ActionResult> Login([FromQuery] string username, [FromQuery] string pwd)
        {
            if (!ModelState.IsValid)
                return Redirect("/loginPage");

            if (username == "admin" && pwd == "pwd")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "HR"),
                    new Claim(ClaimTypes.Email, "HR@gmail.com"),
                    new Claim(ClaimTypes.Role, "HR"),
                    //new Claim("Manager", "HRManager")
                    //new Claim("Manager", "true"),
                    //new Claim("EmploymentDate", "2022-01-01")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("CookieScheme", claimsPrincipal);

                return Redirect("/");
            }

            return Redirect("/loginPage");
        }
    }
}
