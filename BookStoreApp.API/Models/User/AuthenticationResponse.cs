namespace BookStoreApp.API.Models.User
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }

    }
}
