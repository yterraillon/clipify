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
            private readonly IAuthCodeProvider _codeProvider;

            public Handler(IAuthService authService, IAuthCodeProvider codeProvider)
            {
                _authService = authService;
                _codeProvider = codeProvider;
            }

            public Task<AccessTokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                return _authService.GetAccessTokenAsync(_codeProvider.Verifier, request.Code);
            }
        }
    }
}