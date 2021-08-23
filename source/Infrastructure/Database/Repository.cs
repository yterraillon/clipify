using Application;
using AutoMapper;
using Domain.Entities;
using LiteDB;

namespace Infrastructure.Database
{
    public class Repository<T, TEntity, TId> : IRepository<T, TId> where TId : notnull
        where T : Entity
        where TEntity : new()
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TEntity> _collection;

        public Repository(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TEntity>();
        }

        public T Get(TId id) =>
            _mapper.Map<T>(_collection.FindById(ToBsonValue(id)) ?? new TEntity());

        public void Add(T entity) =>
            _collection.Insert(_mapper.Map<TEntity>(entity));

        public bool Remove(TId id) =>
            _collection.Delete(ToBsonValue(id));

        public void Update(T entity) =>
            _collection.Update(_mapper.Map<TEntity>(entity));

        //private static ObjectId ToLiteDbId(TId id) => new(id.ToString());
        private static BsonValue ToBsonValue(TId id) => new (id);
    }
}