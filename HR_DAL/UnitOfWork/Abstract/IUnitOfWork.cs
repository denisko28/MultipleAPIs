using HR_DAL.Repositories.Abstract;

namespace HR_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IBarberRepository BarberRepository { get; }

        IBranchRepository BranchRepository { get; }

        IDayOffRepository DayOffRepository { get; }

        IEmployeeDayOffRepository EmployeeDayOffRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }
    }
}
