namespace FrankLiu_BlazorServer.Controllers;

public class AuthorizationController : ControllerBase
{
    [Route("/simpleAuth")]
    [Authorize]
    public async Task<IActionResult> SimpleAuthorization()
    {       
        return Ok("SimpleAuthorization");
    }

    [Route("/roleAuth")]
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> RoleAuthorization()
    {
        return Ok("Role Authorization");
    }

    [Route("/policyAuth")]
    [Authorize(Policy = "HrManagerRole")]
    public async Task<IActionResult> PolicyAuthorization()
    {
        return Ok("Policy Authorization");
    }

    [Route("/customAuth")]
    [Authorize(Policy = "DirectorSucursala")]
    public async Task<IActionResult> CustomAuthorization()
    {
        return Ok("Custom Authorization");
    }
}
