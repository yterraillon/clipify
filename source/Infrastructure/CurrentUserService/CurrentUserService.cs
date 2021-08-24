using System;
using Application;
using Application.User;
using Domain.Entities;
using Domain.Entities.Spotify;

namespace Infrastructure.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        public SpotifyCredentials SpotifyCredentials { get; }
        private UserProfile CurrentUser { get; }

        // TODO : shouldn't know about UserProfile AND Tokens. Need to fix something here
        public CurrentUserService(IRepository<UserProfile> userRepository, IRepository<Tokens> tokensRepository)
        {
            CurrentUser = userRepository.Get(UserProfile.DefaultUserId);

            var tokens = tokensRepository.Get(Tokens.DefaultTokensId);
            SpotifyCredentials = new SpotifyCredentials
            {
                AccessToken = tokens.AccessToken,
                ExpirationDate = tokens.ExpirationDate,
                UserId = CurrentUser.SpotifyProfile.Id
            };
        }

        public UserProfile GetCurrentUser() => CurrentUser;

        public bool IsUserLoggedIn() => CurrentUser.Username != string.Empty;

        public bool IsUserLoggedInWithSpotify() => SpotifyCredentials.UserId != string.Empty;

        public bool IsSpotifyTokenStillValid() => SpotifyCredentials.ExpirationDate <= DateTime.UtcNow;
    }
}