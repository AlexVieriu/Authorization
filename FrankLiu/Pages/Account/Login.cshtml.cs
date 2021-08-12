using FrankLiu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

                await HttpContext.SignInAsync("CookieScheme", claimsPrincipal);                

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
