using Application.Common.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using LiteDB;

namespace Infrastructure.Database
{
    public class DataReader<TDto, TOut> : IDataReader<TOut>
    where TOut : class
    {
        private readonly IMapper _mapper;

        private readonly ILiteCollection<TDto> _collection;

        public DataReader(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _collection = context.Database.GetCollection<TDto>();
        }

        public IEnumerable<TOut> GetAll()
        {
            var dtos = _collection.FindAll();
            return _mapper.Map<IEnumerable<TOut>>(dtos);
        }
    }
}