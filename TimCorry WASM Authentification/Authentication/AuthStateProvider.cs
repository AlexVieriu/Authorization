using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace TimCorry_WASM_Authentification.Authentication;
public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        if (string.IsNullOrWhiteSpace(token))
            return _anonymous;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
            JWTParser.ParseClaimsFromJWT(token), "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var  authUser = new ClaimsPrincipal(new ClaimsIdentity(
            JWTParser.ParseClaimsFromJWT(token), "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(authUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
