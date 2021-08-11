using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clipify.Application
{
    public interface IRepository<T, in TId> where T : class
    {
        T Get(TId id);

        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        bool Remove(TId id);

        void Update(T entity);

        bool Any(Expression<Func<T, bool>> predicate);
    }
}