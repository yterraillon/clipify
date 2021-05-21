using Clipify.Application.Auth.Requests.TokenRequest.Models;
using Clipify.Application.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests.TokenRequest
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

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (refreshToken is null)
                    return Task.FromResult(new TokenResponse());

                return _authService.RefreshTokenAsync(refreshToken);
            }
        }
    }
}