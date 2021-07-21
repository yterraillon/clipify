using Clipify.Application;
using Clipify.Application.Playlists;
using Clipify.Application.Playlists.Commands.SyncPlaylists;
using Clipify.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Infrastructure.Spotify.Sync
{
    public class SyncService : ISyncService
    {
        private readonly IRepository<Playlist, string> _playlistRepository;

        private readonly IRepository<Track, string> _trackRepository;

        private readonly IRepository<ForkedPlaylist, string> _forkedPlaylistRepository;

        private readonly IPlaylistClient _playlistClient;

        public SyncService(IRepository<Playlist, string> playlistRepository, IRepository<Track, string> trackRepository,
            IRepository<ForkedPlaylist, string> forkedPlaylistRepository, IPlaylistClient playlistClient)
        {
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
            _forkedPlaylistRepository = forkedPlaylistRepository;
            _playlistClient = playlistClient;
        }

        public async Task<bool> SyncPlaylistAsync(User user, string playlistId, CancellationToken cancellationToken = new())
        {
            var localPlaylist = _playlistRepository.Get(playlistId);

            if (localPlaylist.Equals(Playlist.Empty))
                return false;
                
            var playlist = await _playlistClient.GetPlaylistWithTracksAsync(user.AccessToken,
                localPlaylist.PlaylistId, cancellationToken);

            localPlaylist.RegisterLastCheck();
                
            if (!localPlaylist.IsOutdated(playlist.SnapshotId))
                return true;

            localPlaylist.Update(playlist.Name, playlist.SnapshotId);

            foreach (var track in playlist.Tracks)
            {
                if (_trackRepository.Any(t => t.TrackId == track.Id && t.PlaylistId == playlist.Id))
                    continue;
                    
                _trackRepository.Add(Track.Create(track.Id, playlist.Id, track.Uri));
            }
            
            // TODO: Update all forks?

            return true;
        }
    }
}