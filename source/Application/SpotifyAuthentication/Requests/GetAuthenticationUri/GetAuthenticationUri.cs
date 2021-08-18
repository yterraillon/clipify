using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.SpotifyAuthentication.Requests.GetAuthenticationUri
{
    public static class GetAuthenticationUri
    {
        public record Request : IRequest<Response>;

        public record Response(string Uri);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISpotifyAuthenticationUriBuilder _spotifyAuthenticationUriBuilder;

            public Handler(ISpotifyAuthenticationUriBuilder spotifyAuthenticationUriBuilder) => _spotifyAuthenticationUriBuilder = spotifyAuthenticationUriBuilder;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken) =>
                Task.FromResult(new Response(
                    _spotifyAuthenticationUriBuilder.GetAuthorizeUrl(RequiredScopesForApp())
                ));

            private static IEnumerable<string> RequiredScopesForApp() =>
                new List<string>
                {
                    Scopes.PlaylistReadPrivate,
                    Scopes.PlaylistReadCollaborative,
                    Scopes.UserReadPrivate
                };
        }
    }
}