using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBarberRepository BarberRepository { get; }

        public IBranchRepository BranchRepository { get; }

        public IDayOffRepository DayOffRepository { get; }

        public IEmployeeDayOffRepository EmployeeDayOffRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(
            IBarberRepository barberRepository,
            IBranchRepository branchRepository,
            IDayOffRepository dayOffRepository,
            IEmployeeDayOffRepository employeeDayOffRepository,
            IEmployeeRepository employeeRepository)
        {
            BarberRepository = barberRepository;
            BranchRepository = branchRepository;
            DayOffRepository = dayOffRepository;
            EmployeeDayOffRepository = employeeDayOffRepository;
            EmployeeRepository = employeeRepository;
        }
    }
}
