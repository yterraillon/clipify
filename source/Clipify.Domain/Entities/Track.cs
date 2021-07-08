namespace Clipify.Domain.Entities
{
    public class Track : Entity
    {
        public string PlaylistId { get; init; } = string.Empty;
        
        public string TrackId { get; init; } = string.Empty;

        public string PlaylistUri { get; init; } = string.Empty;

        public static Track Create(string trackId, string playlistId, string playlistUri)
            => new()
            {
                PlaylistId = playlistId,
                TrackId = trackId,
                PlaylistUri = playlistId
            };
    }
}