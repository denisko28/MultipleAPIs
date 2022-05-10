using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }

        public override async Task<Branch> GetCompleteEntityAsync(int id)
        {
            var branch = await Table.Include(branch => branch.Employees)
                                     .SingleOrDefaultAsync(branch => branch.Id == id);

            return branch ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
    }
}
