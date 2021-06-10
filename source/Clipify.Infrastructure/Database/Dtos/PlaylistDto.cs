using System;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class PlaylistDto : EntityDto
    {
        public string SnapshotId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime LastCheckedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}