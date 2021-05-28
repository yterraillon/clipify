using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.Models;
using Clipify.Application.Users;
using MediatR;

namespace Clipify.Application.Auth.Requests.GetRefreshToken
{
    public static class GetRefreshToken
    {
        public class Request : IRequest<TokenResponse>
        {

        }

        public class Handler : IRequestHandler<Request, TokenResponse>
        {
            private readonly IAuthService _authService;
            private readonly ICurrentUserService _currentUser;

            public Handler(IAuthService authService, ICurrentUserService currentUser)
            {
                _authService = authService;
                _currentUser = currentUser;
            }

            public Task<TokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var refreshToken = _currentUser.GetCurrentUser()?.RefreshToken;

                return string.IsNullOrEmpty(refreshToken)
                    ? Task.FromResult(TokenResponse.Empty)
                    : _authService.RefreshTokenAsync(refreshToken);
            }
        }
    }
}