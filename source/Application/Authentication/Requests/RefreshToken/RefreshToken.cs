using System.Threading;
using System.Threading.Tasks;
using Application.Authentication.Requests.Models;
using MediatR;

namespace Application.Authentication.Requests.RefreshToken
{
    public static class RefreshToken
    {
        public record Request : IRequest<TokenResponse>;

        public class Handler : IRequestHandler<Request, TokenResponse>
        {
            private readonly ISpotifyTokenService _spotifyTokenService;
            private readonly ICurrentUserService _currentUser;

            public Handler(ISpotifyTokenService spotifyTokenService, ICurrentUserService currentUser)
            {
                _spotifyTokenService = spotifyTokenService;
                _currentUser = currentUser;
            }

            public Task<TokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var refreshToken = _currentUser.GetCurrentUser()?.RefreshToken;

                return string.IsNullOrEmpty(refreshToken)
                    ? Task.FromResult(TokenResponse.Empty)
                    : _spotifyTokenService.RefreshTokenAsync(refreshToken);
            }
        }
    }
}