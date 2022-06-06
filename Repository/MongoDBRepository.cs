using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MongoDBRepository<T> : IMongoDBRepository<T>
    {
        private readonly IMongoCollection<T> collection;
        public MongoDBRepository(MongoConnectionSettings mongodbConnectionSettings)
        {
            var client = new MongoClient(mongodbConnectionSettings.ConnectionString);
            var database = client.GetDatabase(mongodbConnectionSettings.DatabaseName);
            collection = database.GetCollection<T>(mongodbConnectionSettings.CollectionName);


        }

        public IMongoCollection<T> GetCollection()
        {
            return collection;
        }


        public async Task Add(T entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task Update(FilterDefinition<T> filter, T entity)
        {
            await collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(FilterDefinition<T> filter)
        {
            await collection.DeleteOneAsync(filter);
        }
        public async Task<T> Get(FilterDefinition<T> filter)
        {
            var result = await collection.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter)
        {
            var result = await collection.FindAsync(filter);
            return await result.ToListAsync();
        }

    }
}
