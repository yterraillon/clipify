using System;
using System.Collections.Generic;

namespace Clipify.Domain.Entities
{
    public class Playlist : Entity
    {
        public static readonly Playlist Empty = new();

        public string PlaylistId { get; private init; } = string.Empty;

        public string SnapshotId { get; private set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime LastCheckedDate { get; private set; }

        public DateTime LastModifiedDate { get; private set; }

        public IList<string> TrackIds { get; } = new List<string>();

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

        public void Update(string title, string snapshotId)
        {
            Title = title;
            SnapshotId = snapshotId;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void RegisterLastCheck()
        {
            LastCheckedDate = DateTime.UtcNow;
        }

        public bool IsOutdated(string snapshotId) => !snapshotId.Equals(SnapshotId);
    }
}