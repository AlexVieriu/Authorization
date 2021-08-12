using FrankLiu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FrankLiu.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
           
        public void OnGet()
        {     
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Verify the credential
            if (Credential.UserName == "admin" && Credential.Password == "pwd")
            {
                // Creating the security context
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Deparment", "HR"), 
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Manager", "true"),
                    new Claim("EmploymentDate", "2022-01-01")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // make the cookie last for the hole lifespan, even the browser is closed(2:00)
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                await HttpContext.SignInAsync("CookieScheme", claimsPrincipal, authProperties);                

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
