using System;
using System.Threading;
using System.Threading.Tasks;
using Clipify.Application.Auth.Requests;
using Clipify.Application.Users;
using Clipify.Domain.Entities;
using MediatR.Pipeline;

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

            var user = _currentUserService.GetCurrentUser();

            if (user.ExpirationDate <= DateTime.UtcNow)
            {
                var response = await _authService.RefreshTokenAsync(user.RefreshToken);

                user.AccessToken = response.AccessToken;
                user.RefreshToken = response.RefreshToken;
                user.ExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);

                _userRepository.Update(user);
            }
        }
    }
}