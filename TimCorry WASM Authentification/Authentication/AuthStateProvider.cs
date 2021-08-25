using Microsoft.AspNetCore.Components.Authorization;

namespace TimCorry_WASM_Authentification.Authentication;
public class AuthStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}
