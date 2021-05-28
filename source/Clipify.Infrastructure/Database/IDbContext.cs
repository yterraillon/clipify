using LiteDB;

namespace Clipify.Infrastructure.Database
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}