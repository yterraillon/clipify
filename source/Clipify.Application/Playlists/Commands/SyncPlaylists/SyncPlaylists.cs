using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.SyncPlaylists
{
    public static class SyncPlaylists
    {
        public record Command : IRequest<bool>;

        public class Handler : BaseUserHandler, IRequestHandler<Command, bool>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            private readonly IRepository<Track, string> _trackRepository;

            private readonly IRepository<ForkedPlaylist, string> _forkedPlaylistRepository;

            private readonly IPlaylistClient _playlistClient;
            
            public Handler(ICurrentUserService currentUserService, IRepository<Playlist, string> playlistRepository,
                IRepository<Track, string> trackRepository, IRepository<ForkedPlaylist, string> forkedPlaylistRepository,
                IPlaylistClient playlistClient) : base(currentUserService)
            {
                _playlistRepository = playlistRepository;
                _trackRepository = trackRepository;
                _forkedPlaylistRepository = forkedPlaylistRepository;
                _playlistClient = playlistClient;
            }

            /// <inheritdoc />
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var playlists = _playlistRepository.GetAll().ToList();

                // Run all tasks in parallel.
                var results = await Task.WhenAll(playlists.Select(playlist
                    => Task.Run(async () => await SyncPlaylistAsync(playlist.Id, cancellationToken), cancellationToken)));

                return results.All(result => result); // result == true.
            }
            
            private async Task<bool> SyncPlaylistAsync(string playlistId, CancellationToken cancellationToken = new())
            {
                var localPlaylist = _playlistRepository.Get(playlistId);

                if (localPlaylist.Equals(Playlist.Empty))
                    return false;
                
                var playlist = await _playlistClient.GetPlaylistWithTracksAsync(CurrentUser.AccessToken,
                    localPlaylist.PlaylistId, cancellationToken);

                UpdateLocalPlaylist(localPlaylist, playlist);
                SaveNewTracks(localPlaylist.Id, playlist.Tracks);
                UpdateForks(localPlaylist);

                return true;
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
}