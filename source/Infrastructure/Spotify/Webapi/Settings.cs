namespace Infrastructure.Spotify.Webapi
{
    public class Settings
    {
        public string BaseUrl { get; set; } = string.Empty;

        public string ProfileEndpoint { get; set; } = string.Empty;

        public string CurrentUserPlaylistsEndpoint { get; set; } = string.Empty;

        public string UserPlaylistsEndpoint { get; set; } = string.Empty;

        public string PlaylistEndpoint { get; set; } = string.Empty;

        public string PlaylistTracksEndpoint { get; set; } = string.Empty;
    }
}