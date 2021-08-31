using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Spotify.Webapi.Models;
using Infrastructure.Spotify.Webapi.Playlists.Models;

namespace Infrastructure.Spotify.Webapi.Playlists
{
    public interface ISpotifyPlaylistClient
    {
        Task<PagingObject<SimplifiedPlaylistObject>> GetAListOfCurrentUsersPlaylist(string accessToken, CancellationToken cancellationToken);
    }
}