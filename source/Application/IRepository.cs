using Domain.Entities;

namespace Application
{
    public interface IRepository<T>
        where T : Entity
    {
        void Create(T entity);

        T Get(string id);

        void Update(T entity);

        bool Remove(string id);

        //T Get(Expression<Func<T, bool>> predicate);

        //IEnumerable<T> GetAll();

        //IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);

        //bool Any(Expression<Func<T, bool>> predicate);
    }
}