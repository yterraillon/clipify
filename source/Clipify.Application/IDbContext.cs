using Clipify.Domain.Entities;
using LiteDB;

namespace Clipify.Application
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }

        ILiteCollection<User> Users { get; }
    }
}