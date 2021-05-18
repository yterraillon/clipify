using System.IO;
using Clipify.Application;
using Clipify.Domain.Common;
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
            //Database = new LiteDatabase(string.Format(configuration.GetConnectionString("LiteDB"), Directory.GetCurrentDirectory()));
            Database = new LiteDatabase(configuration.GetConnectionString("LiteDB"));

            Users = Database.GetCollection<User>();
        }
    }
}