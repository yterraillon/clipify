using Clipify.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.SyncPlaylists
{
    public interface ISyncService
    {
        Task<bool> SyncPlaylistAsync(User user, string playlistId, CancellationToken cancellationToken = new());
    }
}