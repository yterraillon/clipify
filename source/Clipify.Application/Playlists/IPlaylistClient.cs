using Clipify.Application.Playlists.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists
{
    public interface IPlaylistClient
    {
        Task<PlaylistViewModel> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken = new CancellationToken());

        Task<IEnumerable<PlaylistViewModel>> GetPlaylistsAsync(string token, string userId,
            CancellationToken cancellationToken = new CancellationToken());
    }
}