namespace Clipify.Domain.Entities
{
    public class Track : Entity
    {
        public string PlaylistId { get; init; } = string.Empty;
        
        public string TrackId { get; init; } = string.Empty;

        public string Uri { get; private init; } = string.Empty;

        public static Track Create(string trackId, string playlistId, string trackUri)
            => new()
            {
                PlaylistId = playlistId,
                TrackId = trackId,
                Uri = trackUri
            };
    }
}