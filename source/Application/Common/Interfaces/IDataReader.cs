using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IDataReader<out TOut>
        where TOut : class
    {
        IEnumerable<TOut> GetAll();
    }
}