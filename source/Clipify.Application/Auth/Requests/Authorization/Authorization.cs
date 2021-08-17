using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Auth.Requests.Authorization
{
    using Models;

    public static class Authorization
    {
        public record Request(string State, string Scope) : IRequest<AuthorizationResponse>
        {
            public override string ToString() =>
                $"{nameof(State)}: {State}, {nameof(Scope)}: {Scope}";
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
                => Task.FromResult(new AuthorizationResponse(
                    _authUriBuilder.GetAuthorizeUrl(_codeProvider.Challenge, request.Scope, request.State)
                ));
        }
    }
}