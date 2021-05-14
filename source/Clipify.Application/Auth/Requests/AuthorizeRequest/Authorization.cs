using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests.AuthorizeRequest
{
    public static class Authorization
    {
        public class Request : IRequest<string>
        {
            public string State { get; set; } = string.Empty;

            public string Scope { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly IAuthUriBuilder _authUriBuilder;
            private readonly IAuthCodeProvider _codeProvider;

            public Handler(IAuthUriBuilder authUriBuilder, IAuthCodeProvider codeProvider)
            {
                _authUriBuilder = authUriBuilder;
                _codeProvider = codeProvider;
            }

            public Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_authUriBuilder.GetAuthorizeUrl(_codeProvider.Challenge, request.Scope, request.State));
            }
        }
    }
}