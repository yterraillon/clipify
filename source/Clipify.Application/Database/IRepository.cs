using System.Collections.Generic;

namespace Clipify.Application.Database
{
    public interface IRepository<T, in TId> where T : class
    {
        T Get(TId id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Remove(TId id);
    }
}