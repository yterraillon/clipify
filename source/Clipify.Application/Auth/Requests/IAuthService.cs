using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public Task<AccessTokenResponse> GetAccessTokenAsync(string verifier, string code);
    }
}