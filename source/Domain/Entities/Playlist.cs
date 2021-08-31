using System;

namespace Domain.Entities
{
    public class Playlist : Entity
    {
        public string Service { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string LatestKnownVersion { get; private set; } = string.Empty;
        public string CoverImage { get; private set; } = "about:blank"; // TODO : default image
        public bool IsAClone { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public bool IsVersionUpToDateWithLatest(string latestVersion) => latestVersion == LatestKnownVersion;

        public void DefineAsClone() => IsAClone = true;

        public static Playlist Create(string playlistId, string name, string version, string coverImage,
            bool isAClone, string service) =>
            new()
            {
                Id = playlistId,
                Name = name,
                LatestKnownVersion = version,
                CoverImage = coverImage,
                IsAClone = isAClone,
                LastUpdate = DateTime.Now,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Service = service
            };

        public void UpdateWithLatestVersion(string name, string version, string coverImage)
        {
            Name = name;
            LatestKnownVersion = version;
            CoverImage = coverImage;
            LastUpdate = DateTime.Now;
        }
    }
}