namespace Permission_Based.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class UserRolesController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRolesController(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager,
                               RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index(string userId)
    {
        var viewModel = new List<UserRolesViewModel>();
        var user = await _userManager.FindByNameAsync(userId);
        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRolesViewModel = new UserRolesViewModel { RoleName = role.Name };

            if (await _userManager.IsInRoleAsync(user, role.Name))
                userRolesViewModel.Selected = true;
            else
                userRolesViewModel.Selected = false;

            viewModel.Add(userRolesViewModel);
        }

        var model = new ManageUserRolesViewModel()
        {
            UserId = userId,
            UserRoles = viewModel
        };

        return View(model);
    }


    // In case one admin tries to change the roles of another.
    //[HttpPost]
    public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(id);
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
        var currentUser = await _userManager.GetUserAsync(User);
        await _signInManager.RefreshSignInAsync(currentUser);
        await Seeds.DefaultUsers.SeedSuperAdminAsync(_userManager, _roleManager); // ??

        return RedirectToAction("Index", new { userId = id });

    }
}
