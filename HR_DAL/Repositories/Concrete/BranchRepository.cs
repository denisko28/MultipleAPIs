using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
    }
}
