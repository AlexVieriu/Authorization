namespace Permission_Based.Controllers;

[Authorize(Roles ="SuperAdmin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        if(roleName is not null)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
        }
        return RedirectToPage("Index");
    }
}
