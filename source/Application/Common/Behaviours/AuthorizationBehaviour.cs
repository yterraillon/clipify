using System;
using System.Threading;
using System.Threading.Tasks;
using Application.SpotifyAuthentication.Commands.Login;
using Application.SpotifyAuthentication.Queries.GetAuthenticationUri;
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
            if(IsAuthorizationRequired(request) && !_currentUserService.IsUserLoggedIn())
                // TODO : renvoie sur la page de login
                throw new UnauthorizedAccessException();

            return next();
        }

        private static bool IsAuthorizationRequired(TRequest request)
            => request switch
            {
                GetAuthenticationUri.Request => false,
                Login.Request => false,
                _ => true
            };
    }
}