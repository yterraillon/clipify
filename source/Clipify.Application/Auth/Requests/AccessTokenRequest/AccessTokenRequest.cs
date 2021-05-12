using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.AccessTokenRequest.Models;
using MediatR;

namespace Clipify.Application.Auth.Requests.AccessTokenRequest
{
    public static class AccessTokenRequest
    {
        public class Request : IRequest<AccessTokenResponse>
        {
            public string Code { get; set; } = String.Empty;
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