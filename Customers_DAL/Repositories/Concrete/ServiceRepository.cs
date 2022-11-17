using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.Repositories.Concrete
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }

        public override async Task InsertAsync(Service entity)
        {
            await Table.AddAsync(entity);
        }
    }
}
