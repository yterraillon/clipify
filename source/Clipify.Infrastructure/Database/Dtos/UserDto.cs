using LiteDB;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class UserDto
    {
        public ObjectId Id { get; set; } = ObjectId.Empty;

        public string Username { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public int ExpiresIn { get; set; }
    }
}