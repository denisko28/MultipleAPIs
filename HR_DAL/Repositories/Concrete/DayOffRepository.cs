using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class DayOffRepository : GenericRepository<DayOff>, IDayOffRepository
    {
        public DayOffRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
