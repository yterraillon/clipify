using System;

namespace Infrastructure.Database.Dtos
{
    public class PlaylistDto : EntityDto
    {
        public string Service { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LatestKnownVersion { get; set; } = string.Empty;
        public string CoverImage { get; set; } = "about:blank"; // TODO : default image
        public bool IsAClone { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}