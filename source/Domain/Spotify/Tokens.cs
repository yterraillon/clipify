using System;

namespace Domain.Spotify
{
    public class Tokens
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public static Tokens Empty => new();

        public Tokens() { }

        public Tokens(string accessToken, string refreshToken, int expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn);
        }
    }
}