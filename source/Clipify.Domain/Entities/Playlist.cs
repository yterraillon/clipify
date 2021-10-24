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
            Created = DateTime.Now,
            UpdatedBy = userId,
            Updated = DateTime.Now,
            LastCheckedDate = DateTime.Now,
            LastModifiedDate = DateTime.Now,
            Title = title
        };

        public void Update(string title, string snapshotId)
        {
            Title = title;
            SnapshotId = snapshotId;
            LastModifiedDate = DateTime.Now;
        }

        public void RegisterLastCheck()
        {
            LastCheckedDate = DateTime.Now;
        }

        public bool IsOutdated(string snapshotId) => !snapshotId.Equals(SnapshotId);
    }
}