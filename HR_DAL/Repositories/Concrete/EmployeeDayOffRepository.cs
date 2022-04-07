using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class EmployeeDayOffRepository : GenericRepository<EmployeeDayOff>, IEmployeeDayOffRepository
    {
        public EmployeeDayOffRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
