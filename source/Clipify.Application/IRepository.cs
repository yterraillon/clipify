using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clipify.Application
{
    public interface IRepository<T, in TId> where T : class
    {
        T Get(TId id);

        T Get(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Remove(TId id);

        void Update(T entity);
    }
}