namespace Application.Authentication.Requests.Authorization
{
    public interface ISpotifyAuthenticationUriBuilder
    {
        public string GetAuthorizeUrl(string scope, string state);
    }
}