using Clipify.Application;
using Clipify.Domain.Entities;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Clipify.Infrastructure.Database
{
    public class DbContext : IDbContext
    {
        public LiteDatabase Database { get; }

        public ILiteCollection<User> Users { get; }

        public DbContext(IConfiguration configuration)
        {
            Database = new LiteDatabase(configuration.GetConnectionString("LiteDB"));

            Users = Database.GetCollection<User>();

            Users.EnsureIndex(x => x.Id, true);
        }
    }
}