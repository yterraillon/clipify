using System.Threading.Tasks;
using Domain.Entities.Spotify;

namespace Application.SpotifyAuthentication.Requests
{
    public interface ISpotifyTokenService
    {
        public Task<Tokens> GetAccessTokenAsync(string code);

        public Task<Tokens> RefreshTokenAsync(string refreshToken);
    }
}