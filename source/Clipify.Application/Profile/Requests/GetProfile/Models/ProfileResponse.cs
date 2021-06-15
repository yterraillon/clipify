using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Clipify.Application.Profile.Requests.GetProfile.Models
{
    public class ProfileResponse
    {
        public string Country { get; set; } = string.Empty;

        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonProperty("external_urls")]
        public IEnumerable<ExternalUrl> ExternalUrls { get; } = Enumerable.Empty<ExternalUrl>();

        public Followers Followers { get; set; } = new Followers();

        public string Id { get; set; } = string.Empty;

        public IEnumerable<Image> Images { get; } = Enumerable.Empty<Image>();

        public string Product { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Uri { get; set; } = string.Empty;

        public static ProfileResponse Empty => new ProfileResponse();
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