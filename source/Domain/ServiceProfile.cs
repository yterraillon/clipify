namespace Domain
{
    public class ServiceProfile
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public static ServiceProfile Empty() => new();

        public bool IsLoggedIn() => Id != null && Id != string.Empty;
    }
}