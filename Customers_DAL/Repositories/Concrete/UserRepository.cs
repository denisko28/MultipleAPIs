using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }


        public override Task<User> GetCompleteEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
