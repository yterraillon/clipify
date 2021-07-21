using Clipify.Application.Common;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.SyncPlaylists
{
    public static class SyncPlaylist
    {
        public class Command : IRequest<bool>
        {
            public string PlaylistId { get; set; } = string.Empty;
        }

        public class Handler : BaseUserHandler, IRequestHandler<Command, bool>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            private readonly IRepository<Track, string> _trackRepository;

            private readonly IPlaylistClient _playlistClient;

            private readonly IPlaylistService _playlistService;
            
            public Handler(IRepository<Playlist, string> playlistRepository, IPlaylistClient playlistClient,
                ICurrentUserService currentUserService, IRepository<Track, string> trackRepository,
                IPlaylistService playlistService) : base(currentUserService)
            {
                _playlistRepository = playlistRepository;
                _playlistClient = playlistClient;
                _trackRepository = trackRepository;
                _playlistService = playlistService;
            }

            /// <inheritdoc />
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var localPlaylist = _playlistRepository.Get(request.PlaylistId);

                if (localPlaylist.Equals(Playlist.Empty))
                    return false;
                
                var playlist = await _playlistClient.GetPlaylistWithTracksAsync(CurrentUser.AccessToken,
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
                
                // TODO: Get Tracks.
                // TODO: Update ForkedPlaylist.
                
                return true;
            }
        }
    }
}