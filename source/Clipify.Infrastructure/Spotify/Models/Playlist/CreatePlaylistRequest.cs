namespace Clipify.Infrastructure.Spotify.Models.Playlist
{
    public class CreatePlaylistRequest
    {
        public string Name { get; set; } = string.Empty;
        
        public bool Public { get; set; }
        
        public bool Collaborative { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}