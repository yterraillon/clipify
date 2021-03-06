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
        public record Command(string PlaylistId, string SnapshotId, string Title) : IRequest
        {
            /// <inheritdoc />
            public override string ToString()
            {
                return $"{nameof(PlaylistId)}: {PlaylistId}, {nameof(SnapshotId)}: {SnapshotId}, {nameof(Title)}: {Title}";
            }
        }

        public class Handler : BaseUserHandler, IRequestHandler<Command>
        {
            private readonly IPlaylistService _playlistService;
            
            private readonly IPlaylistClient _playlistClient;

            public Handler(ICurrentUserService currentUserService, IPlaylistClient playlistClient,
                IPlaylistService playlistService) : base(currentUserService)
            {
                _playlistClient = playlistClient;
                _playlistService = playlistService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = await _playlistClient.GetPlaylistWithTracksAsync(
                    CurrentUser.AccessToken, request.PlaylistId, cancellationToken);

                var playlist = Playlist.Create(
                    request.PlaylistId,
                    request.SnapshotId,
                    CurrentUser.UserId,
                    request.Title
                );

                _playlistService.CreatePlaylistWithTracks(playlist, response.Tracks);

                return Unit.Value;
            }
        }
    }
}