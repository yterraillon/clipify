using AutoMapper;
using Clipify.Application.Database;
using LiteDB;
using System.Collections.Generic;

namespace Clipify.Infrastructure.Database.Repositories
{
    public class Repository<T, TEntity, TId> : IRepository<T, TId>
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
            => Mapper.Map<T>(Collection.FindById(new BsonValue(id)) ?? new TEntity());

        public IEnumerable<T> GetAll()
            => Mapper.Map<IEnumerable<T>>(Collection.FindAll());

        public void Remove(TId id)
            => Collection.Delete(new BsonValue(id));
    }
}