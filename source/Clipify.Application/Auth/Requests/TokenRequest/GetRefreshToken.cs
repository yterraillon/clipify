using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.TokenRequest.Models;
using MediatR;

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
            private readonly IDbContext _context;

            public Handler(IAuthService authService, IDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public Task<TokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var refreshToken = _context.Users.FindOne(x => x.AccessToken.Any())?.RefreshToken;

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (refreshToken is null)
                    return Task.FromResult(new TokenResponse());

                return _authService.RefreshTokenAsync(refreshToken);
            }
        }
    }
}