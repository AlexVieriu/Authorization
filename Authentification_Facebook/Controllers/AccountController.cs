namespace Authentification_Facebook.Controllers;

[AllowAnonymous, Route("account")]
public class AccountController : Controller
{

    [Route("facebook-login")]
    public IActionResult FacebookLogin()
    {
        // user will be rederected once we are loged in to facebook
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("FacebookResponse") };

        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    [Route("facebook-response")]
    public async Task<IActionResult> FacebookResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer, 
            claim.Type, 
            claim.Value
        });      

        return Json(claims);
    }
}
