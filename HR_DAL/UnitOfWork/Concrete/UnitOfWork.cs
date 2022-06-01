using HR_DAL.MongoRepositories.Abstract;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAppointmentRepository AppointmentRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IUserRepository UserRepository { get; }

        public IBarberRepository BarberRepository { get; }

        public IBranchRepository BranchRepository { get; }

        public IDayOffRepository DayOffRepository { get; }

        public IEmployeeDayOffRepository EmployeeDayOffRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }
        
        public IBranchMongoRepository BranchMongoRepository { get; }

        public UnitOfWork(
            IAppointmentRepository appointmentRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IBarberRepository barberRepository,
            IBranchRepository branchRepository,
            IDayOffRepository dayOffRepository,
            IEmployeeDayOffRepository employeeDayOffRepository,
            IEmployeeRepository employeeRepository, 
            IBranchMongoRepository branchMongoRepository)
        {
            AppointmentRepository = appointmentRepository;
            CustomerRepository = customerRepository;
            UserRepository = userRepository;
            BarberRepository = barberRepository;
            BranchRepository = branchRepository;
            DayOffRepository = dayOffRepository;
            EmployeeDayOffRepository = employeeDayOffRepository;
            EmployeeRepository = employeeRepository;
            BranchMongoRepository = branchMongoRepository;
        }
    }
}
