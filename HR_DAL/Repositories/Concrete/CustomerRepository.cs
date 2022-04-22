using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
