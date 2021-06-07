using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Playlists.Models;
using Clipify.Application.Users;
using MediatR;

namespace Clipify.Application.Playlists.Requests.GetPlaylist
{
    public static class GetPlaylist
    {
        public class Request : IRequest<PlaylistResponse>
        {
            public string PlaylistId { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, PlaylistResponse>
        {
            private readonly IPlaylistClient _client;
            private readonly ICurrentUserService _currentUser;

            public Handler(IPlaylistClient client, ICurrentUserService currentUser)
            {
                _client = client;
                _currentUser = currentUser;
            }

            public async Task<PlaylistResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = _currentUser.GetCurrentUser();

                if (string.IsNullOrEmpty(user.UserId) || string.IsNullOrEmpty(user.AccessToken))
                    return PlaylistResponse.Empty;

                if (!string.IsNullOrEmpty(request.PlaylistId))
                {
                    return await _client.GetPlaylistAsync(user.AccessToken, user.UserId, request.PlaylistId,
                        cancellationToken);
                }

                return await _client.GetPlaylistsAsync(user.AccessToken, user.UserId, cancellationToken);
            }
        }
    }
}