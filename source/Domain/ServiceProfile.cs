namespace Domain
{
    public class ServiceProfile
    {
        public string ServiceName { get; init; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public static ServiceProfile Empty(string service) => new()
        {
            ServiceName = service
        };

        public bool IsLoggedIn() => Id != string.Empty;
    }
}