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
                return RedirectToPage("/loginPage");

            if(username == "admin"&& pwd == "pwd")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, ""),
                    new Claim("Department", "HR"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2022-01-01"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("CookieScheme", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return RedirectToPage("/loginPage");
        }
    }
}
