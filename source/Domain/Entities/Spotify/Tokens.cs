using System;

namespace Domain.Entities.Spotify
{
    public class Tokens : UniqueEntity
    {
        public const string DefaultTokensId = "default";

        public Tokens() : base(DefaultTokensId)
        {
        }

        public string AccessToken { get; private set; } = string.Empty;
        public string RefreshToken { get; private init; } = string.Empty;
        public DateTime ExpirationDate { get; private set; }

        public bool AreExpired() => ExpirationDate >= DateTime.UtcNow;

        public void RefreshAccessToken(string accessToken, int expiresIn)
        {
            AccessToken = accessToken;
            ExpirationDate = ComputeExpirationDate(expiresIn);
            Updated = DateTime.UtcNow;
        }

        public static Tokens Empty => new();

        public static Tokens Create(string accessToken, string refreshToken, int expiresIn) =>
            new()
            {
                Id = DefaultTokensId,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationDate = ComputeExpirationDate(expiresIn),
            };

        private static DateTime ComputeExpirationDate(int expiresIn) => DateTime.UtcNow.AddSeconds(expiresIn);
    }
}