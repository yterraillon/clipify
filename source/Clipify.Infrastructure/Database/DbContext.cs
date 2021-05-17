using System.IO;
using Clipify.Application;
using Clipify.Domain.Common;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Clipify.Infrastructure.Database
{
    public class DbContext : IDbContext
    {
        public LiteDatabase Database { get; }


        public DbContext(IConfiguration configuration)
        {
            Database = new LiteDatabase(string.Format(configuration.GetConnectionString("LiteDB"), Directory.GetCurrentDirectory()));
        }
    }
}