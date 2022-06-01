using System.Threading.Tasks;
using HR_DAL.Connection.Concrete;
using HR_DAL.Entities;
using HR_DAL.Exceptions;
using HR_DAL.MongoRepositories.Abstract;
using MongoDB.Driver;

namespace HR_DAL.MongoRepositories.Concrete
{
    public class BranchMongoRepository: IBranchMongoRepository
    {
        private readonly IMongoCollection<Branch> collection;

        public BranchMongoRepository(MongoDbContext mongoDbContext)
        {
            collection = mongoDbContext.Collection<Branch>();;
        }

        public async Task InsertAsync(Branch branch)
        {
            await collection.InsertOneAsync(branch);
        }

        public async Task UpdateAsync(Branch branch)
        {
            var filter = Builders<Branch>.Filter.Eq(c => c.Id, branch.Id);
            var branchExists = await collection.Find(filter).AnyAsync();
            
            if (!branchExists)
                throw new EntityNotFoundException(nameof(Branch), branch.Id);
            
            await collection.ReplaceOneAsync(filter, branch);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var filter = Builders<Branch>.Filter.Eq(c => c.Id, id);
            var branchExists = await collection.Find(filter).AnyAsync();
            
            if (!branchExists)
                throw new EntityNotFoundException(nameof(Branch), id);
            
            await collection.DeleteOneAsync(filter);
        }
    }
}