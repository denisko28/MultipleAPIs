using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class PossibleTimeRepository : IPossibleTimeRepository
    {
        private readonly BarbershopDbContext dbContext;

        private readonly DbSet<PossibleTime> table;

        public PossibleTimeRepository(BarbershopDbContext dBContext)
        {
          this.dbContext = dBContext;
          table = this.dbContext.Set<PossibleTime>();
        }

        public async Task<IEnumerable<PossibleTime>> GetAllAsync()
        {
          return await table.ToListAsync();
        }

        public async Task<IEnumerable<PossibleTime>> GetAllAvailableAsync()
        {
          return await table.Where(possibleTime => possibleTime.Available == true).ToListAsync();
        }

        public async Task UpdateAsync(PossibleTime entity)
        {
          await Task.Run(() => table.Update(entity));
        }
    }
}
