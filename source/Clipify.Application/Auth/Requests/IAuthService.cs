using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.Models;

namespace Clipify.Application.Auth.Requests
{
    public interface IAuthService
    {
        public Task<TokenResponse> GetAccessTokenAsync(string verifier, string code);

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken);
    }
}