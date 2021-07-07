namespace Clipify.Domain.Entities
{
    public class Track : Entity
    {
        public string PlaylistId { get; init; } = string.Empty;
        public string TrackId { get; init; } = string.Empty;

        public static Track Create(string trackId, string playlistId)
            => new()
            {
                PlaylistId = playlistId,
                TrackId = trackId
            };
    }
}