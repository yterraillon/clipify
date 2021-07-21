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
        public record Command(string PlaylistId) : IRequest<bool>;

        public class Handler : BaseUserHandler, IRequestHandler<Command, bool>
        {
            private readonly ISyncService _syncService;
            public Handler(ICurrentUserService currentUserService, ISyncService syncService) : base(currentUserService)
            {
                _syncService = syncService;
            }

            /// <inheritdoc />
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return _syncService.SyncPlaylistAsync(CurrentUser, request.PlaylistId, cancellationToken);
            }
        }
    }
}