using System;
using Application;
using Application.Common;
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
        public CurrentUserService(IRepository<UserProfile, Guid> userRepository, IRepository<Tokens, Guid> tokensRepository)
        {
            CurrentUser = userRepository.Get(Constants.DefaultId);

            var tokens = tokensRepository.Get(Constants.DefaultId);
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