using System;

namespace Clipify.Domain.Entities
{
    public class Playlist : Entity
    {
        public string SnapshotId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime LastCheckedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}