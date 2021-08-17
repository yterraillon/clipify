using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication.Requests.Authorization;
using Application.Authentication.Requests.GetAccessToken;
using MediatR;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService)
            => _currentUserService = currentUserService;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!IsAuthorizationRequired(request))
                return next();

            if (!_currentUserService.IsUserLoggedIn())
                throw new UnauthorizedAccessException();

            return next();
        }

        private static bool IsAuthorizationRequired(TRequest request)
            => request switch
            {
                //CreateLocalUser.Command => false,
                GetSpotifyAuthenticationUri.Request => false,
                GetAccessToken.Request => false,
                _ => true
            };
    }
}