using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Playlists.Queries.GetPlaylist;

namespace Application.Playlists
{
    public interface ISpotifyPlaylistService
    {
        Task<IEnumerable<PlaylistInformation>> GetAllPlaylists(CancellationToken cancellationToken);

        Task<PlaylistViewModel> GetPlaylist(string playlistId, CancellationToken cancellationToken);
    }
}