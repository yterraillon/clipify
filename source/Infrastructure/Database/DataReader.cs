using Application.Common.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using LiteDB;

namespace Infrastructure.Database
{
    public class DataReader<TEntity> : IDataReader<TEntity>
    where TEntity : Entity
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TEntity> _collection;

        public DataReader(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TEntity>();
        }

        public IEnumerable<TEntity> GetAll() =>
            _mapper.Map<IEnumerable<TEntity>>(_collection.FindAll());
    }
}