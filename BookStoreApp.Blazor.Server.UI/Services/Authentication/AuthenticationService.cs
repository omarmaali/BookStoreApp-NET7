using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClient = httpClient;
            this._localStorageService = localStorageService;
            this._authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(UserLoginDto loginModel)
        {
            var response = await _httpClient.LoginAsync(loginModel);

            // Store tocken
            await _localStorageService.SetItemAsync("accessToekn", response.Token);
            // Change app auth state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
            return true;
        
        }

        public async Task Logout()
        {
           await  ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();

        }
    }
}
