using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.UserProfile
{
    public class UserProfileResponse
    {
        public string Country { get; set; } = string.Empty;

        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonProperty("external_urls")]
        public IEnumerable<ExternalUrl> ExternalUrls { get; } = Enumerable.Empty<ExternalUrl>();

        public Followers Followers { get; set; } = new();

        [JsonProperty("id")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("images")]
        public IList<Image> Images { get; } = new List<Image>();

        public string Product { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;

        public static UserProfileResponse Empty => new();
    }

    public class ExternalUrl
    {
        [JsonProperty("spotify")]
        public string Url { get; set; } = string.Empty;
    }

    public class Followers
    {
        [JsonProperty("href")]
        public string? Link { get; set; }

        [JsonProperty("total")]
        public int Count { get; set; }
    }

    public class Image
    {
        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;

        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}