using HR_DAL.MongoRepositories.Abstract;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IUserRepository UserRepository { get; }

        IBarberRepository BarberRepository { get; }

        IBranchRepository BranchRepository { get; }

        IDayOffRepository DayOffRepository { get; }

        IEmployeeDayOffRepository EmployeeDayOffRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IBranchMongoRepository BranchMongoRepository { get; }
    }
}
