using TimCorry_WASM_Authentification.Models;

namespace TimCorry_WASM_Authentification.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel user);
        Task Logout(AuthenticationUserModel user);
    }
}