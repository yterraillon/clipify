using System.Text.Json.Serialization;

namespace Clipify.Infrastructure.Spotify.Models.User
{
    public class PublicUserResponse
    {
        [JsonIgnore]
        public static PublicUserResponse Empty => new();
    }
}