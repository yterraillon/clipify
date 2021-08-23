using System;

namespace Infrastructure.Database.Dtos
{
    public class SpotifyTokensDto : EntityDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
    }
}