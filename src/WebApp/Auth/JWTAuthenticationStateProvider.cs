using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using WebApp.Helpers;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.Auth
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient httpClient;
        private readonly string TOKEN_KEY = "TOKENKEY";
        private static AuthenticationState Anonymous => new(new ClaimsPrincipal(new ClaimsIdentity()));

        public JWTAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient)
        {
            _js = js;
            this.httpClient = httpClient;
        }

        public async Task Login(string token)
        {
            await _js.SetInLocalStorage(TOKEN_KEY, token);

            var authState = BuildAuthenticationState(token);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await _js.RemoveItem(TOKEN_KEY);

            httpClient.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.GetFromLocalStorage(TOKEN_KEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            return BuildAuthenticationState(token);
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimFromJwt(token), "jwt")));
        }

        #region Methods private Aux
        private static IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuesPairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes!);

            keyValuesPairs!.TryGetValue(ClaimTypes.Role, out object? roles);

            if (roles != null)
            {
                if (roles.ToString()!.Trim().StartsWith("["))
                {
                    var parseRoles =  JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                    foreach (var parseRole in parseRoles!)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parseRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }

                keyValuesPairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuesPairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; 
                    break;
                case 3: base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }       
        #endregion
    }
}