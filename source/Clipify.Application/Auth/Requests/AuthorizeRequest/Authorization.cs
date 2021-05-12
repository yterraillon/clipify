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
            private readonly IAuthService _authService;

            public Handler(IAuthService authService)
            {
                _authService = authService;
            }

            public Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_authService.GetAuthorizeUrl(request));
            }
        }
    }
}