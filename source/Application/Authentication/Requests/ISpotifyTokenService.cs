using System.Threading.Tasks;
using Application.Authentication.Requests.Models;

namespace Application.Authentication.Requests
{
    public interface ISpotifyTokenService
    {
        public Task<TokenResponse> GetAccessTokenAsync(string code);

        public Task<TokenResponse> RefreshTokenAsync(string refreshToken);
    }
}