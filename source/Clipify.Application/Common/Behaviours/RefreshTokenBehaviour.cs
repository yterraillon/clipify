using Clipify.Application.Auth.Requests;
using Clipify.Application.Auth.Requests.Authorization;
using Clipify.Application.Auth.Requests.GetAccessToken;
using Clipify.Application.Users;
using Clipify.Application.Users.Commands.CreateLocalUser;
using Clipify.Domain.Entities;
using MediatR.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clipify.Application.Common.Behaviours
{
    public class RefreshTokenBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IRepository<User, string> _userRepository;

        private readonly IAuthService _authService;

        public RefreshTokenBehaviour(ICurrentUserService currentUserService, IAuthService authService, IRepository<User, string> userRepository)
        {
            _currentUserService = currentUserService;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsUserLoggedIn())
                return;

            if (!IsVerificationRequired(request))
                return;

            var user = _currentUserService.GetCurrentUser();

            if (user.TokenExpirationDate <= DateTime.UtcNow)
            {
                var response = await _authService.RefreshTokenAsync(user.RefreshToken);

                user.AccessToken = response.AccessToken;
                user.RefreshToken = response.RefreshToken;
                user.TokenExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);

                _userRepository.Update(user);
            }
        }
        
        private static bool IsVerificationRequired(TRequest request)
            => request switch
            {
                CreateLocalUser.Command => false,
                Authorization.Request => false,
                GetAccessToken.Request => false,
                _ => true
            };
    }
}