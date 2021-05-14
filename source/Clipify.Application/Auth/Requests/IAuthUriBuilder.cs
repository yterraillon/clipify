namespace Clipify.Application.Auth.Requests
{
    public interface IAuthUriBuilder
    {
        public string GetAuthorizeUrl(string challenge, string scope, string state);
    }
}