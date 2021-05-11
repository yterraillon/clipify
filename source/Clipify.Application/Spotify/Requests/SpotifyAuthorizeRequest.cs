using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common.Interfaces;
using MediatR;

namespace Clipify.Application.Spotify.Requests
{
    public static class SpotifyAuthorizeRequest
    {
        public class Request : IRequest<string>
        {
            public string State { get; set; } = String.Empty;

            public string Scope { get; set; } = String.Empty;
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly ISpotifyAuthService _spotifyAuthService;

            public Handler(ISpotifyAuthService spotifyAuthService)
            {
                _spotifyAuthService = spotifyAuthService;
            }

            public Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_spotifyAuthService.GetAuthorizeUrl(request));
            }
        }
    }
}