namespace Application.Playlists
{
    public record PlaylistInformation
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string CoverImage { get; set; } = "about:blank";
    }
}