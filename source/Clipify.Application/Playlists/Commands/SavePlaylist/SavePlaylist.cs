using Clipify.Application.Common;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Commands.SavePlaylist
{
    public static class SavePlaylist
    {
        public class Command : IRequest
        {
            public string PlaylistId { get; set; } = string.Empty;

            public string SnapshotId { get; set; } = string.Empty;

            public string Title { get; set; } = string.Empty;

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{nameof(PlaylistId)}: {PlaylistId}, {nameof(SnapshotId)}: {SnapshotId}, {nameof(Title)}: {Title}";
            }
        }

        public class Handler : BaseUserHandler, IRequestHandler<Command>
        {
            private readonly IRepository<Playlist, string> _playlistRepository;

            private readonly IRepository<Track, string> _trackRepository;

            private readonly IPlaylistClient _playlistClient;

            public Handler(IRepository<Playlist, string> playlistRepository, ICurrentUserService currentUserService,
                IPlaylistClient playlistClient, IRepository<Track, string> trackRepository) : base(currentUserService)
            {
                _playlistRepository = playlistRepository;
                _playlistClient = playlistClient;
                _trackRepository = trackRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await _playlistClient.GetPlaylistWithTracksAsync(CurrentUser.AccessToken, request.PlaylistId,
                    cancellationToken);

                var playlist = Playlist.Create(
                    request.PlaylistId,
                    request.SnapshotId,
                    CurrentUser.UserId,
                    request.Title
                );


                foreach (var track in response.Tracks)
                {
                    var t = Track.Create(track.Id, playlist.Id);
                    
                    playlist.TrackIds.Add(t.Id);
                    _trackRepository.Add(t);
                }
                
                _playlistRepository.Add(playlist);

                return Unit.Value;
            }
        }
    }
}