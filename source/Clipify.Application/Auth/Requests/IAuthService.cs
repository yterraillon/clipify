using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public string GetAuthorizeUrl(AuthorizeRequest.Authorization.Request request);

        public Task<AccessTokenResponse> GetAccessTokenAsync(string code);
    }
}