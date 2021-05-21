using Clipify.Application.Spotify.Clients;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users;

namespace Clipify.Application.Spotify.Requests.ProfileRequest
{
    public static class Profile
    {
        public class Request : IRequest<ProfileResponse>
        {

        }

        public class Handler : IRequestHandler<Request, ProfileResponse>
        {
            private readonly ISpotifyClient _client;
            private readonly ICurrentUserService _currentUser;

            public Handler(ISpotifyClient client, ICurrentUserService currentUser)
            {
                _client = client;
                _currentUser = currentUser;
            }

            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = _currentUser.GetCurrentUser()?.AccessToken;

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (string.IsNullOrEmpty(token))
                    return Task.FromResult(new ProfileResponse());

                return _client.GetUserProfileAsync(token, cancellationToken);
            }
        }
    }
}