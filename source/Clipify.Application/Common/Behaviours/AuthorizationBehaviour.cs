using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users;
using MediatR;

namespace Clipify.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_currentUserService.IsUserLoggedIn())
                throw new UnauthorizedAccessException();

            return await next();
        }
    }
}