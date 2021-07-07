using System;
using System.Collections;
using System.Collections.Generic;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class PlaylistDto : EntityDto
    {
        public string PlaylistId { get; set; } = string.Empty;

        public string SnapshotId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public IList<string> TrackIds { get; set; } = new List<string>();

        public DateTime LastCheckedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}