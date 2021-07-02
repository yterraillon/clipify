using Clipify.Application.Auth.Requests.Authorization;
using Clipify.Application.Auth.Requests.GetAccessToken;
using Clipify.Application.Users;
using Clipify.Application.Users.Commands.CreateLocalUser;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Users.Requests;

namespace Clipify.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService)
            => _currentUserService = currentUserService;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (IsAuthorizationNotRequired(request))
                return next();

            if (!_currentUserService.IsUserLoggedIn())
                throw new UnauthorizedAccessException();

            return next();
        }

        private static bool IsAuthorizationNotRequired(TRequest request)
            => request switch
            {
                CreateLocalUser.Command => true,
                Authorization.Request => true,
                GetAccessToken.Request => true,
                GetUser.Request => true,
                _ => false
            };
    }
}