using Clipify.Application.Playlists.Models;
using Clipify.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.SyncPlaylists
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

            UpdateLocalPlaylist(localPlaylist, playlist);
            SaveNewTracks(localPlaylist.Id, playlist.Tracks);
            UpdateForks(localPlaylist);

            return true;
        }

        public async Task<bool> SyncAllPlaylistsAsync(User user, CancellationToken cancellationToken = new())
        {
            var playlists = _playlistRepository.GetAll().ToList();

            // Run all tasks in parallel.
            // Maybe LiteDB is not thread safe..?
            var results = await Task.WhenAll(playlists.Select(playlist
                => Task.Run(async () => await SyncPlaylistAsync(user, playlist.Id, cancellationToken), cancellationToken)));

            return results.All(result => result); // result == true.
        }

        private void UpdateLocalPlaylist(Playlist localPlaylist, PlaylistViewModel playlist)
        {
            localPlaylist.RegisterLastCheck();

            if (!localPlaylist.IsOutdated(playlist.SnapshotId))
                return;

            localPlaylist.Update(playlist.Name, playlist.SnapshotId);
            _playlistRepository.Update(localPlaylist);
        }

        private void UpdateForks(Playlist localPlaylist)
        {
            var forks = _forkedPlaylistRepository.GetAll(f => f.OriginalPlaylistId == localPlaylist.Id);

            foreach (var fork in forks)
            {
                if (!fork.IsOutdated(localPlaylist.SnapshotId))
                    continue;
                
                fork.RegisterLastSync(localPlaylist.SnapshotId);
                _forkedPlaylistRepository.Update(fork);
            }
        }

        private void SaveNewTracks(string playlistId, IEnumerable<TrackViewModel> tracks)
        {
            foreach (var track in tracks)
            {
                if (_trackRepository.Any(t => t.TrackId == track.Id && t.PlaylistId == playlistId))
                    continue;

                _trackRepository.Add(Track.Create(track.Id, playlistId, track.Uri));
            }
        }
    }
}