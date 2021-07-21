namespace Clipify.Infrastructure.Database.Dtos
{
    public class TrackDto : EntityDto
    {
        public string PlaylistId { get; set; } = string.Empty;
        
        public string TrackId { get; set; } = string.Empty;
        
        public string Uri { get; set; } = string.Empty;
    }
}