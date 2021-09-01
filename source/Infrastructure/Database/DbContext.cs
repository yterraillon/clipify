using Infrastructure.Database.Dtos;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database
{
    public class DbContext : IDbContext
    {
        public LiteDatabase Database { get; }

        public DbContext(IConfiguration configuration)
        {
            Database = new LiteDatabase(configuration.GetConnectionString("LiteDB"));

            Database.GetCollection<UserDto>()
                .EnsureIndex(x => x.Id, true);

            Database.GetCollection<SpotifyTokensDto>()
                .EnsureIndex(x => x.Id, true);

            Database.GetCollection<PlaylistDto>()
                .EnsureIndex(x => x.Id, true);
        }
    }
}