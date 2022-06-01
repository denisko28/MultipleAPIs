using HR_DAL.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HR_DAL.Connection.Concrete
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext(string mongoUrl)
        {
            MapClasses();
            database = InitDbInstance(mongoUrl);
        }

        private static IMongoDatabase InitDbInstance(string mongoUrl)
        {
            var url = new MongoUrl(mongoUrl);
            var client = new MongoClient(mongoUrl);
            return client.GetDatabase(url.DatabaseName);
        }

        private static void MapClasses()
        {
            BsonClassMap.RegisterClassMap<Branch>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity: class
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}