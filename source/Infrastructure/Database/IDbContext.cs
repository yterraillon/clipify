using LiteDB;

namespace Infrastructure.Database
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}