using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Authentication.Requests.Authorization
{
    public static class GetSpotifyAuthenticationUri
    {
        public record Request(string State, string Scope) : IRequest<Response>
        {
            public override string ToString() =>
                $"{nameof(State)}: {State}, {nameof(Scope)}: {Scope}";
        }

        public record Response(string Url);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ISpotifyAuthenticationUriBuilder _spotifyAuthenticationUriBuilder;

            public Handler(ISpotifyAuthenticationUriBuilder spotifyAuthenticationUriBuilder) => _spotifyAuthenticationUriBuilder = spotifyAuthenticationUriBuilder;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            // TODO : should generate Scope and Or State here
                => Task.FromResult(new Response(
                    _spotifyAuthenticationUriBuilder.GetAuthorizeUrl(request.Scope, request.State)
                ));
        }
    }
}