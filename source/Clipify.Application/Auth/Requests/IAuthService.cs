using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public string GetAuthorizeUrl(AuthorizeRequest.AuthorizeRequest.Request request);

        public Task<AccessTokenResponse> GetAccessTokenAsync(string code);
    }
}