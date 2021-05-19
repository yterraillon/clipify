using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clipify.Application.Spotify.Requests.ProfileRequest.Models
{
    public class ProfileResponse
    {
        public ProfileResponse()
        {
            ExternalUrls = new List<ExternalUrl>();
            Followers = new Followers();
            Images = new List<Image>();
        }

        public string Country { get; set; } = string.Empty;

        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonProperty("external_urls")]
        public ICollection<ExternalUrl> ExternalUrls { get; }

        public Followers Followers { get; set; }

        public string Id { get; set; } = string.Empty;

        public ICollection<Image> Images { get; }

        public string Product { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;
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
        public int? Height { get; set; }

        public string Url { get; set; } = string.Empty;

        public int? Width { get; set; }
    }
}