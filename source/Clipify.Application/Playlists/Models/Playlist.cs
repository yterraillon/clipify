namespace Clipify.Application.Playlists.Models
{
    public class Playlist
    {
        public string Id { get; set; } = string.Empty;

        public bool Collaborative { get; set; }

        public string Description { get; set; } = string.Empty;

    }
}