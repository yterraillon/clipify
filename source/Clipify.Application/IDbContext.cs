using LiteDB;

namespace Clipify.Application
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}