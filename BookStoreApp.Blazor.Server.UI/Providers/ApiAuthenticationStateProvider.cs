using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        public ApiAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this._localStorageService = localStorageService;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user =  new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await _localStorageService.GetItemAsync<string>("accessToekn");

            if (savedToken == null)
            {
                return new AuthenticationState(user);
            }
                        
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            if(tokenContent.ValidTo <DateTime.Now)
            {
                return new AuthenticationState(user);
            }
            var claims = tokenContent.Claims;

             user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }               

        public async Task LoggedIn()
        {
            var savedToken = await _localStorageService.GetItemAsync<string>("accessToekn");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims;
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await _localStorageService.RemoveItemAsync("accessToekn");
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
