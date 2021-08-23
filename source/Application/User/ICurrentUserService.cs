using System;
using Domain.Entities;

namespace Application.User
{
    public interface ICurrentUserService
    {
        SpotifyCredentials SpotifyCredentials { get; }

        UserProfile GetCurrentUser();

        bool IsUserLoggedIn();

        bool IsUserLoggedInWithSpotify();

        bool IsSpotifyTokenStillValid();
    }

    public class SpotifyCredentials
    {
        public string UserId { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }
    }
}