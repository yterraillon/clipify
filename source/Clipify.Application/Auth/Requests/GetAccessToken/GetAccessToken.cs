﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Clipify.Application.Auth.Requests.GetAccessToken
{
    using Models;

    public static class GetAccessToken
    {
        public class Request : IRequest<TokenResponse>
        {
            public string Code { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Request, TokenResponse>
        {
            private readonly IAuthService _authService;
            private readonly IAuthCodeProvider _codeProvider;

            public Handler(IAuthService authService, IAuthCodeProvider codeProvider)
            {
                _authService = authService;
                _codeProvider = codeProvider;
            }

            public Task<TokenResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                return _authService.GetAccessTokenAsync(_codeProvider.Verifier, request.Code);
            }
        }
    }
}