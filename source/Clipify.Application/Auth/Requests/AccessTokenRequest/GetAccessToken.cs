using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests.AccessTokenRequest
{
    public static class GetAccessToken
    {
        public class Request : IRequest<AccessTokenResponse>
        {
            public string Code { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, AccessTokenResponse>
        {
            private readonly IAuthService _authService;

            public Handler(IAuthService authService)
            {
                _authService = authService;
            }

            public Task<AccessTokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                return _authService.GetAccessTokenAsync(request.Code);
            }
        }
    }
}