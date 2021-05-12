using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using Clipify.Application.Auth.Requests.AuthorizeRequest;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public string GetAuthorizeUrl(Authorization.Request request);

        public Task<AccessTokenResponse> GetAccessTokenAsync(string code);
    }
}