using System;
using System.Collections.Generic;

namespace Clipify.Domain.Entities
{
    public class Playlist : Entity
    {
        public string PlaylistId { get; private init; } = string.Empty;

        public string SnapshotId { get; private init; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime LastCheckedDate { get; private init; }

        public DateTime LastModifiedDate { get; private init; }

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
    }
}