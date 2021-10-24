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

        public bool AreExpired() => ExpirationDate <= DateTime.Now;

        public void RefreshAccessToken(string accessToken, int expiresIn)
        {
            AccessToken = accessToken;
            ExpirationDate = ComputeExpirationDate(expiresIn);
            Updated = DateTime.Now;
        }

        public static Tokens Empty => new();

        public static Tokens Create(string accessToken, string refreshToken, int expiresIn) =>
            new()
            {
                Id = DefaultTokensId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpirationDate = ComputeExpirationDate(expiresIn),
            };

        private static DateTime ComputeExpirationDate(int expiresIn) => DateTime.Now.AddSeconds(expiresIn);
    }
}