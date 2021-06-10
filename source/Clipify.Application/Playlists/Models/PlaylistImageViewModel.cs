namespace Clipify.Application.Playlists.Models
{
    public class PlaylistImageViewModel
    {
        public string Url { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }

        public static PlaylistImageViewModel Empty => new PlaylistImageViewModel();
    }
}