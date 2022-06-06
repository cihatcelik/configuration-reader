using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMongoDBRepository<T>
    {
        Task Add(T entity);
        Task Update(FilterDefinition<T> filter, T entity);
        Task Delete(FilterDefinition<T> filter);
        Task<T> Get(FilterDefinition<T> filter);
        Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter);
        IMongoCollection<T> GetCollection();
    }
}
