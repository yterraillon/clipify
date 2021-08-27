using System.Threading.Tasks;
using Domain.Entities.Spotify;

namespace Application.SpotifyAuthentication
{
    public interface ITokensService
    {
        Task<Tokens> GetSpotifyTokens();
    }
}