using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists
{
    public interface IPlaylistClient
    {
        Task<PlaylistViewModel> GetPlaylistAsync(string token, string userId, string playlistId,
            CancellationToken cancellationToken = new());

        Task<IEnumerable<PlaylistViewModel>> GetPlaylistsAsync(string token, string userId,
            CancellationToken cancellationToken = new());

        Task<PlaylistViewModel> GetPlaylistWithTracksAsync(string token, string playlistId,
            CancellationToken cancellationToken = new());

        Task<PlaylistViewModel> CreatePlaylistAsync(string token, string userId, string name,
            CancellationToken cancellationToken = new());

        Task<string> AddTracksToPlaylistAsync(string token, string playlistId, IEnumerable<Track> tracks,
            CancellationToken cancellationToken = new());
    }
}