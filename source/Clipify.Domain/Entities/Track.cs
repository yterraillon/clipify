namespace Clipify.Domain.Entities
{
    public class Track : Entity
    {
        public string TrackId { get; init; } = string.Empty;

        public static Track Create(string trackId)
            => new()
            {
                TrackId = trackId
            };
    }
}