using System;
using System.Threading;
using System.Threading.Tasks;
using Application.SpotifyAuthentication.Requests;
using Application.SpotifyAuthentication.Requests.GetAuthenticationUri;
using Application.SpotifyAuthentication.Requests.Login;
using Application.User;
using MediatR.Pipeline;

namespace Application.Common.Behaviours
{
    // TODO : merge with authorize
    public class RefreshTokenBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        //private readonly IRepository<User, string> _userRepository;

        private readonly ISpotifyTokenService _spotifyTokenService;

        public RefreshTokenBehaviour(ICurrentUserService currentUserService, ISpotifyTokenService spotifyTokenService)//, IRepository<User, string> userRepository)
        {
            _currentUserService = currentUserService;
            _spotifyTokenService = spotifyTokenService;
            //_userRepository = userRepository;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            //if (!_currentUserService.IsUserLoggedIn())
            //    return;

            //if (!IsVerificationRequired(request))
            //    return;

            //var user = _currentUserService.GetCurrentUser();

            //if (user.TokenExpirationDate <= DateTime.UtcNow)
            //{
            //    var spotifyTokens = await _spotifyTokenService.RefreshTokenAsync(user.RefreshToken);

            //    user.AccessToken = spotifyTokens.AccessToken;
            //    user.RefreshToken = spotifyTokens.RefreshToken;
            //    user.TokenExpirationDate = DateTime.UtcNow.AddSeconds(spotifyTokens.ExpiresIn);

            //    //_userRepository.Update(user);
            //}
        }

        private static bool IsVerificationRequired(TRequest request)
            => request switch
            {
                //CreateLocalUser.Command => false,
                GetAuthenticationUri.Request => false,
                Login.Request => false,
                _ => true
            };
    }
}