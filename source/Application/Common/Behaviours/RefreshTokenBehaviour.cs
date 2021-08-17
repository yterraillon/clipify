using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication.Requests;
using Application.Authentication.Requests.Authorization;
using Application.Authentication.Requests.GetAccessToken;
using Domain.Entities;
using MediatR.Pipeline;

namespace Application.Common.Behaviours
{
    public class RefreshTokenBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IRepository<User, string> _userRepository;

        private readonly ISpotifyTokenService _spotifyTokenService;

        public RefreshTokenBehaviour(ICurrentUserService currentUserService, ISpotifyTokenService spotifyTokenService, IRepository<User, string> userRepository)
        {
            _currentUserService = currentUserService;
            _spotifyTokenService = spotifyTokenService;
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
                var response = await _spotifyTokenService.RefreshTokenAsync(user.RefreshToken);

                user.AccessToken = response.AccessToken;
                user.RefreshToken = response.RefreshToken;
                user.TokenExpirationDate = DateTime.UtcNow.AddSeconds(response.ExpiresIn);

                _userRepository.Update(user);
            }
        }

        private static bool IsVerificationRequired(TRequest request)
            => request switch
            {
                //CreateLocalUser.Command => false,
                GetSpotifyAuthenticationUri.Request => false,
                GetAccessToken.Request => false,
                _ => true
            };
    }
}