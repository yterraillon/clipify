using System;
using Domain.Entities.Spotify;

namespace Infrastructure.Database.Dtos
{
    public class SpotifyTokensDto : EntityDto
    {
        public SpotifyTokensDto() => Id = Tokens.DefaultTokensId;

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
    }
}