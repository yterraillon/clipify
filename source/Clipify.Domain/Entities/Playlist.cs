using System;

namespace Clipify.Domain.Entities
{
    public class Playlist : Entity
    {
        public string PlaylistId { get; private set; } = string.Empty;

        public string SnapshotId { get; private set; } = string.Empty;

        public string Title { get; private set; } = string.Empty;

        public DateTime LastCheckedDate { get; private set; }

        public DateTime LastModifiedDate { get; private set; }

        public static Playlist Create(string playlistId, string snapshotId, string userId, string title) => new Playlist
        {
            PlaylistId = playlistId,
            SnapshotId = snapshotId,
            CreatedBy = userId,
            Created = DateTime.UtcNow,
            UpdatedBy = userId,
            Updated = DateTime.UtcNow,
            LastCheckedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow,
            Title = title
        };
    }
}