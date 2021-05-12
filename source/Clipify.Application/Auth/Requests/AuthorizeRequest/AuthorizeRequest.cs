using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Clipify.Application.Auth.Requests.AuthorizeRequest
{
    public static class AuthorizeRequest
    {
        public class Request : IRequest<string>
        {
            public string State { get; set; } = String.Empty;

            public string Scope { get; set; } = String.Empty;
        }

        public class Handler : IRequestHandler<Request, string>
        {
            private readonly IAuthService _spotifyAuthService;

            public Handler(IAuthService spotifyAuthService)
            {
                _spotifyAuthService = spotifyAuthService;
            }

            public Task<string> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_spotifyAuthService.GetAuthorizeUrl(request));
            }
        }
    }
}