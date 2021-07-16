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
            private readonly IRepository<ForkedPlaylist, string> _forkedPlaylistRepository;

            private readonly IPlaylistClient _playlistClient;

            public Handler(IRepository<ForkedPlaylist, string> forkedPlaylistRepository, IPlaylistClient playlistClient,
                ICurrentUserService currentUserService) : base(currentUserService)
            {
                _forkedPlaylistRepository = forkedPlaylistRepository;
                _playlistClient = playlistClient;
            }

            /// <inheritdoc />
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var localPlaylist = _forkedPlaylistRepository.Get(request.PlaylistId);
                var playlist = await _playlistClient.GetPlaylistAsync(CurrentUser.AccessToken,
                    CurrentUser.UserId, localPlaylist.OriginalPlaylistId, cancellationToken);

                if (!localPlaylist.IsOutdated(playlist.SnapshotId))
                    return true;

                // TODO: Get Tracks.
                // TODO: Update ForkedPlaylist.
                // TODO: Update original Playlist?
                
                localPlaylist.RegisterLastSync(playlist.SnapshotId);

                return true;
            }
        }
    }
}