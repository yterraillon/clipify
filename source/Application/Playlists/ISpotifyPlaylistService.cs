using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.Playlists
{
    public interface ISpotifyPlaylistService
    {
        Task<IEnumerable<PlaylistInformation>> GetAllPlaylists(CancellationToken cancellationToken);
    }
}