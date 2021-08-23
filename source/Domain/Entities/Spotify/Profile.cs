namespace Domain.Entities.Spotify
{
    public class Profile
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public static Profile Create(string username, string id, Tokens tokens) =>
            new()
            {
                Id = id,
                UserName = username
            };

        public static Profile Empty => new();
    }
}