using System;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class ForkedPlaylistDto : EntityDto
    {
        public string Name { get; set; } = string.Empty;
        
        public string SnapshotId { get; set; } = string.Empty;

        public string OriginalPlaylistId { get; set; } = string.Empty;

        public DateTime LastSync { get; set; }
    }
}