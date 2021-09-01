using Application.Common.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using LiteDB;

namespace Infrastructure.Database
{
    public class DataReader<TEntity, TDto> : IDataReader<TEntity>
    where TEntity : Entity
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TDto> _collection;

        public DataReader(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TDto>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var dtos = _collection.FindAll();
            return _mapper.Map<IEnumerable<TEntity>>(dtos);
        }
    }
}