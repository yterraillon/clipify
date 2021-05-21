using Clipify.Application.Database;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;

namespace Clipify.Infrastructure.Database.Repositories
{
    public class Repository<TResult, TEntity, TId> : IRepository<TResult, TEntity, TId>
        where TResult : class
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

        public void Add(TEntity entity)
            => Collection.Insert(entity);

        public TResult Get(TId id)
            => Mapper.Map<TResult>(Collection.FindById(new BsonValue(id)) ?? new TEntity());

        public IEnumerable<TResult> GetAll()
            => Mapper.Map<IEnumerable<TResult>>(Collection.FindAll());

        public void Remove(TId id)
            => Collection.Delete(new BsonValue(id));
    }
}