using System;

namespace Clipify.Domain.Entities
{
    public class ForkedPlaylist : Entity
    {
        public string SnapshotId { get; private set; } = string.Empty;

        public string OriginalPlaylistId { get; private set; } = string.Empty;

        public DateTime LastSync { get; private set; }

        public static ForkedPlaylist Create(string playlistId, string originalPlaylistId) => new ForkedPlaylist
        {
            Id = playlistId,
            OriginalPlaylistId = originalPlaylistId,
            Created = DateTime.Now,
            LastSync = DateTime.Now,
            Updated = DateTime.Now
        };

        public bool IsOutdated(string originalPlaylistSnapshotId) => !originalPlaylistSnapshotId.Equals(SnapshotId);

        public void RegisterLastSync(string latestSnapshotId)
        {
            SnapshotId = latestSnapshotId;
            LastSync = DateTime.Now;
        }
    }
}