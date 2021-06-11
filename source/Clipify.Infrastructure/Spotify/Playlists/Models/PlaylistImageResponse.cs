namespace Clipify.Infrastructure.Spotify.Playlists.Models
{
    public class PlaylistImageResponse
    {
        public string Url { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }

        public static PlaylistImageResponse Empty => new PlaylistImageResponse();
    }
}