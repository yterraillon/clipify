using System;
using AutoMapper;
using LiteDB;
using System.Collections.Generic;
using System.Linq.Expressions;
using Clipify.Application;

namespace Clipify.Infrastructure.Database.Repositories
{
    public class Repository<T, TEntity, TId> : IRepository<T, TId> where TId : notnull
        where T : class
        where TEntity : new()
    {
        protected readonly IMapper Mapper;
        protected readonly IDbContext Context;

        protected readonly ILiteCollection<TEntity> Collection;

        public Repository(IMapper mapper, IDbContext context)
        {
            Mapper = mapper;
            Context = context;

            Collection = Context.Database.GetCollection<TEntity>();
        }

        public void Add(T entity)
            => Collection.Insert(Mapper.Map<TEntity>(entity));

        public T Get(TId id)
            => Mapper.Map<T>(Collection.FindById(ToLiteDbId(id)) ?? new TEntity());

        public T Get(Expression<Func<T, bool>> predicate)
        {
            var expr = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);

            return Mapper.Map<T>(Collection.FindOne(expr) ?? new TEntity());
        }

        public IEnumerable<T> GetAll()
            => Mapper.Map<IEnumerable<T>>(Collection.FindAll());

        public bool Remove(TId id)
            => Collection.Delete(ToLiteDbId(id));

        public void Update(T entity)
            => Collection.Update(Mapper.Map<TEntity>(entity));
        
        private static ObjectId ToLiteDbId(TId id) => new ObjectId(id.ToString());
    }
}