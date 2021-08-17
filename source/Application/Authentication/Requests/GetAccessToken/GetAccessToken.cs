using System.Threading;
using System.Threading.Tasks;
using Application.Authentication.Requests.Models;
using MediatR;

namespace Application.Authentication.Requests.GetAccessToken
{
    public static class GetAccessToken
    {
        public record Request(string Code) : IRequest<TokenResponse>
        {
            public override string ToString() => $"{nameof(Code)}: {Code}";
        }

        public class Handler : IRequestHandler<Request, TokenResponse>
        {
            private readonly ISpotifyTokenService _spotifyTokenService;

            public Handler(ISpotifyTokenService spotifyTokenService) => _spotifyTokenService = spotifyTokenService;

            public Task<TokenResponse> Handle(Request request, CancellationToken cancellationToken)
                => _spotifyTokenService.GetAccessTokenAsync(request.Code);
        }
    }
}