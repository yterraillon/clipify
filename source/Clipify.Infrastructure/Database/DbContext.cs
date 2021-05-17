using Clipify.Application;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace Clipify.Infrastructure.Database
{
    public class DbContext : IDbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LiteDB");
        }
    }
}