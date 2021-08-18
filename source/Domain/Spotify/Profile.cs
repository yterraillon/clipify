namespace Domain.Spotify
{
    public class Profile
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        //public Tokens Tokens { get; set; } = Tokens.Empty;

        public static Profile Create(string username, string id, Tokens tokens) =>
            new()
            {
                Id = id,
                //Tokens = tokens,
                UserName = username
            };

        public static Profile Empty => new();
    }
}