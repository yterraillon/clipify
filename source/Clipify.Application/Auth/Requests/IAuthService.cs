using Clipify.Application.Auth.Requests.TokenRequest.Models;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public Task<TokenResponse> GetAccessTokenAsync(string verifier, string code);

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken);
    }
}