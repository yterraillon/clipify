using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists.Models;

namespace Clipify.Application.Playlists
{
    public interface IPlaylistClient
    {
        Task<PlaylistResponse> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken = new CancellationToken());

        Task<PlaylistResponse> GetPlaylistAsync(string token, string userId,
            CancellationToken cancellationToken = new CancellationToken());
    }
}