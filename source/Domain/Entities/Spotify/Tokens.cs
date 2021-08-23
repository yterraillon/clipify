using System;

namespace Domain.Entities.Spotify
{
    public class Tokens : Entity
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public static Tokens Empty => new();

        //public Tokens() { }

        public static Tokens Create(string accessToken, string refreshToken, int expiresIn) =>
            new()
            {
                Id = new Guid(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn),
            };
    }
}