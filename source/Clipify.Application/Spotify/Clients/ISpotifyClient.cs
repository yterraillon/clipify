using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Spotify.Clients
{
    public interface ISpotifyClient
    {
        Task<ProfileResponse> GetUserProfileAsync(string token,
            CancellationToken cancellationToken = new CancellationToken());
    }
}