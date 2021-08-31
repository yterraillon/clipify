using System.Collections.Generic;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IDataReader<out T>
        where T : Entity
    {
        IEnumerable<T> GetAll();
    }
}