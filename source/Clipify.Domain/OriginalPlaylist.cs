using System;
using System.Collections.Generic;
using System.Linq;

namespace Clipify.Domain
{
    public class OriginalPlaylist
    {
        public string Id { get; private set; } = string.Empty;
        public string SnapshotId { get; private set; } = string.Empty;

        public IEnumerable<Track> Tracks { get; } = Enumerable.Empty<Track>();

        public static OriginalPlaylist Create(string id, string snapshotId) => new OriginalPlaylist
        {
            Id = id,
            SnapshotId = snapshotId,
        };

        public void AddTracks(IEnumerable<Track> tracks)
        {
            foreach (var track in tracks)
            {
                Tracks.Append(track);
            }
        }

        public IEnumerable<Track> GetTracksUpdatedSince(DateTime date) => Tracks.Where(t => t.AddedTime > date);
    }

    public class Track
    {
        public string Id { get; set; } = string.Empty;
        public DateTime AddedTime { get; set; }
    }
}