using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Common.Interfaces;
using Clipify.Application.Common.Models;
using MediatR;

namespace Clipify.Application.Spotify.Requests
{
    public static class SpotifyAccessTokenRequest
    {
        public class Request : IRequest<SpotifyAuthResponse>
        {
            public string Code { get; set; } = String.Empty;
        }

        public class Handler : IRequestHandler<Request, SpotifyAuthResponse>
        {
            private readonly ISpotifyAuthService _spotifyAuthService;

            public Handler(ISpotifyAuthService spotifyAuthService)
            {
                _spotifyAuthService = spotifyAuthService;
            }

            public Task<SpotifyAuthResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                return _spotifyAuthService.GetAccessTokenAsync(request.Code);
            }
        }

    }
}