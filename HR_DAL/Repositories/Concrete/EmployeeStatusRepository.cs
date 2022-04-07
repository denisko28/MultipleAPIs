using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class EmployeeStatusRepository : GenericRepository<EmployeeStatus>, IEmployeeStatusRepository
    {
        public EmployeeStatusRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
