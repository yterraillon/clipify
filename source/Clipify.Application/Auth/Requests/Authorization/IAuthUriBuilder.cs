namespace Clipify.Application.Auth.Requests.Authorization
{
    public interface IAuthUriBuilder
    {
        public string GetAuthorizeUrl(string challenge, string scope, string state);
    }
}