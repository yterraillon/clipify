using System;
using Newtonsoft.Json;

namespace Clipify.Application.Common.Models
{
    public class SpotifyAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = String.Empty;

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = String.Empty;

        [JsonProperty("scope")]
        public string Scope { get; set; } = String.Empty;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = String.Empty;
    }
}