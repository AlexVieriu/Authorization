using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrankLiu.Pages.UserRoles
{
    [Authorize(Policy = "MustBelongToHRDeparment")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
