using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests.AuthorizeRequest.Models;

namespace Clipify.Application.Auth.Requests.AuthorizeRequest
{
    public static class Authorization
    {
        public class Request : IRequest<AuthorizationResponse>
        {
            public string State { get; set; } = string.Empty;

            public string Scope { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, AuthorizationResponse>
        {
            private readonly IAuthUriBuilder _authUriBuilder;
            private readonly IAuthCodeProvider _codeProvider;

            public Handler(IAuthUriBuilder authUriBuilder, IAuthCodeProvider codeProvider)
            {
                _authUriBuilder = authUriBuilder;
                _codeProvider = codeProvider;
            }

            public Task<AuthorizationResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new AuthorizationResponse
                {
                    Url = _authUriBuilder.GetAuthorizeUrl(_codeProvider.Challenge, request.Scope, request.State)
                });
            }
        }
    }
}