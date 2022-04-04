namespace Authn_Blazor.Controller;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    [Route("/login")]
    public async Task<IActionResult> Login([FromQuery] string user, [FromQuery] string pwd)
    {
        if(user == "ina" && pwd == "funduletz")
        {
            var claims = new List<Claim>()
            {
                new Claim("username", user),
                new Claim(ClaimTypes.NameIdentifier, user),
                new Claim(ClaimTypes.Name, "facutza mica")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect("/secured");
        }
        else
        {
            return Unauthorized("The User or Password are incorrect");
        }
        
    }

    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}
