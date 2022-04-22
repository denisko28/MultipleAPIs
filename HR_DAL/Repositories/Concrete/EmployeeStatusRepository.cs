using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class EmployeeStatusRepository : GenericRepository<EmployeeStatus>, IEmployeeStatusRepository
    {
        public EmployeeStatusRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
