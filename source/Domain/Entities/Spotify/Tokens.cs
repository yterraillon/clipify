using System;

namespace Domain.Entities.Spotify
{
    public class Tokens : UniqueEntity
    {
        public const string DefaultTokensId = "default";

        public Tokens() : base(DefaultTokensId)
        {
        }

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public static Tokens Empty => new();

        public static Tokens Create(string accessToken, string refreshToken, int expiresIn) =>
            new()
            {
                Id = DefaultTokensId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationDate = DateTime.UtcNow.AddSeconds(expiresIn),
            };
    }
}