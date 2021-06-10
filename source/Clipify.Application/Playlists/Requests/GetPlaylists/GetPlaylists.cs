using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using MediatR;

namespace Clipify.Application.Playlists.Requests.GetPlaylists
{
    public static class GetPlaylists
    {
        public class Request : IRequest<IEnumerable<PlaylistViewModel>>
        {
        }

        public class Handler : IRequestHandler<Request, IEnumerable<PlaylistViewModel>>
        {
            private readonly ICurrentUserService _currentUserService;

            private readonly IPlaylistClient _playlistClient;

            public Handler(ICurrentUserService currentUserService, IPlaylistClient playlistClient)
            {
                _currentUserService = currentUserService;
                _playlistClient = playlistClient;
            }

            public async Task<IEnumerable<PlaylistViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                if (!_currentUserService.IsUserLoggedIn())
                    return Enumerable.Empty<PlaylistViewModel>();

                var user = _currentUserService.GetCurrentUser();

                return await _playlistClient.GetPlaylistsAsync(user.AccessToken, user.UserId, cancellationToken);
            }
        }
    }
}