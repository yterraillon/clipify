using Application;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Database.Dtos;
using LiteDB;

namespace Infrastructure.Database
{
    public class Repository<T, TDto> : IRepository<T>
        where T : Entity
        where TDto : EntityDto, new()
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TDto> _collection;

        public Repository(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TDto>();
        }

        public void Create(T entity) =>
            _collection.Insert(_mapper.Map<TDto>(entity));

        public T Get(string id) =>
            _mapper.Map<T>(_collection.FindOne(c => c.Id == id) ?? new TDto());

        public void Update(T entity) =>
            _collection.Update(_mapper.Map<TDto>(entity));

        public bool Remove(string id) =>
            _collection.Delete(id);
    }
}