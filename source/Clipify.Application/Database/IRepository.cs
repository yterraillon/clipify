using System.Collections.Generic;

namespace Clipify.Application.Database
{
    public interface IRepository<out TResult, in TEntity, in TId> where TResult : class
    {
        TResult Get(TId id);

        IEnumerable<TResult> GetAll();

        void Add(TEntity entity);

        void Remove(TId id);
    }
}