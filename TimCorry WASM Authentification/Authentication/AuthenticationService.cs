namespace TimCorry_WASM_Authentification.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient client,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorage)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel user)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("grant_type", "password"),
            new KeyValuePair<string,string>("username", user.Email),
            new KeyValuePair<string,string>("password", user.Password),
        });

        var authResult = await _client.PostAsync("https://localhost:5001/token", data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode == false)
        {
            return null;
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
            authContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync("authToken", result.Access_Token);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Access_Token);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("berear", result.Access_Token);

        return result;
    }

    public async Task Logout(AuthenticationUserModel user)
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}
