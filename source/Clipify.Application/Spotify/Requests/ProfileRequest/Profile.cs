using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Spotify.Clients;
using Clipify.Application.Spotify.Requests.ProfileRequest.Models;
using MediatR;

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
            private readonly IDbContext _context;

            public Handler(ISpotifyClient client, IDbContext context)
            {
                _client = client;
                _context = context;
            }

            public Task<ProfileResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = _context.Users.FindOne(x => x.AccessToken.Any())?.AccessToken;

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (string.IsNullOrEmpty(token))
                    return Task.FromResult(new ProfileResponse());

                return _client.GetUserProfileAsync(token, cancellationToken);
            }
        }
    }
}