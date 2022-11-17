using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.Repositories.Concrete
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }

        public override async Task InsertAsync(Branch entity)
        {
            await Table.AddAsync(entity);
        }
    }
}
