namespace Clipify.Application.Auth.Requests
{
    public interface IAuthUriBuilder
    {
        public string GetAuthorizeUrl(string scope, string state);
    }
}