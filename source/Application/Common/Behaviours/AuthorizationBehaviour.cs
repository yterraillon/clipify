using System;
using System.Threading;
using System.Threading.Tasks;
using Application.SpotifyAuthentication.Requests.GetAuthenticationUri;
using Application.SpotifyAuthentication.Requests.Login;
using Application.User;
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
                GetAuthenticationUri.Request => false,
                Login.Request => false,
                _ => true
            };
    }
}