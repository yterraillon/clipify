namespace Clipify.Application.Playlists.Models
{
    public class TrackViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public int TrackNumber { get; set; }

        public string Uri { get; set; } = string.Empty;
    }
}