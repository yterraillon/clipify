using LiteDB;

namespace Clipify.Application.Database
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}