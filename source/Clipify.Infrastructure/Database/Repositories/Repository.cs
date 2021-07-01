using AutoMapper;
using Clipify.Application;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clipify.Infrastructure.Database.Repositories
{
    public class Repository<T, TEntity, TId> : IRepository<T, TId> where TId : notnull
        where T : class
        where TEntity : new()
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TEntity> _collection;

        public Repository(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TEntity>();
        }

        public void Add(T entity)
            => _collection.Insert(_mapper.Map<TEntity>(entity));

        public T Get(TId id)
            => _mapper.Map<T>(_collection.FindById(ToLiteDbId(id)) ?? new TEntity());

        public T Get(Expression<Func<T, bool>> predicate)
        {
            var expr = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);

            return _mapper.Map<T>(_collection.FindOne(expr) ?? new TEntity());
        }

        public IEnumerable<T> GetAll()
            => _mapper.Map<IEnumerable<T>>(_collection.FindAll());

        public bool Remove(TId id)
            => _collection.Delete(ToLiteDbId(id));

        public void Update(T entity)
            => _collection.Update(_mapper.Map<TEntity>(entity));

        private static ObjectId ToLiteDbId(TId id) => new(id.ToString());
    }
}