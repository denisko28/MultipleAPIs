using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Services_Domain.Entities;

namespace Services_Infrastructure
{
    public class MongoDbContext
    {
        public readonly IMongoDatabase Database;

        public MongoDbContext(string mongoUrl)
        {
            InitDbSettings();
            MapClasses();
            Database = InitDbInstance(mongoUrl);
        }

        private static IMongoDatabase InitDbInstance(string mongoUrl)
        {
            var url = new MongoUrl(mongoUrl);
            var client = new MongoClient(mongoUrl);
            return client.GetDatabase(url.DatabaseName);
        }

        private static void InitDbSettings()
        {
            //var pack = new ConventionPack { new CamelCaseElementNameConvention() };
            // ConventionRegistry.Register("camel case", pack, _ => true);
        }

        private static void MapClasses()
        {
            BsonClassMap.RegisterClassMap<Counters>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(m => m.SequenceValue).SetElementName("sequence_value");
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<Service>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(c => c.Price).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
                cm.UnmapMember(c => c.ServiceDiscounts);
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<ServiceDiscount>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Service);
                cm.UnmapMember(c => c.Branch);
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity: class
        {
            return Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}