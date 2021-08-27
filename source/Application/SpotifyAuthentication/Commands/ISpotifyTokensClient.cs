using System.Threading.Tasks;

namespace Application.SpotifyAuthentication.Commands
{
    public interface ISpotifyTokensClient
    {
        public Task<(string accessToken, string refreshToken, int expiresIn)> GetAccessTokenAsync(string code);

        public Task<(string accessToken, int expiresIn)> RefreshTokenAsync(string refreshToken);
    }
}