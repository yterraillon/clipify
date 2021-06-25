using Clipify.Application.Common;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Playlists.Requests.GetPlaylists
{
    public static class GetPlaylists
    {
        public record Request : IRequest<IEnumerable<PlaylistViewModel>>;

        public class Handler : BaseUserHandler, IRequestHandler<Request, IEnumerable<PlaylistViewModel>>
        {
            private readonly IPlaylistClient _playlistClient;

            public Handler(ICurrentUserService currentUserService, IPlaylistClient playlistClient) : base(currentUserService)
                => _playlistClient = playlistClient;

            public Task<IEnumerable<PlaylistViewModel>> Handle(Request request, CancellationToken cancellationToken)
                => _playlistClient.GetPlaylistsAsync(CurrentUser.AccessToken, CurrentUser.UserId, cancellationToken);
        }
    }
}