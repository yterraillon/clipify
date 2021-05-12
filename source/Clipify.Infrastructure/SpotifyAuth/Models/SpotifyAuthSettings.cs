namespace Clipify.Infrastructure.SpotifyAuth.Models
{
    public class SpotifyAuthSettings
    {
        public string AuthorizeUrl { get; set; } = string.Empty;

        public string AccessTokenUrl { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;
    }
}