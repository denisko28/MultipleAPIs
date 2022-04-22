using System;
using System.Data;
using HR_DAL.Connection.Abstract;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IConnectionFactory factoryConnection;
        private readonly IDbConnection connection;
        private IDbTransaction transaction;

        public IAppointmentRepository AppointmentRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IUserRepository UserRepository { get; }

        public IBarberRepository BarberRepository { get; }

        public IBranchRepository BranchRepository { get; }

        public IDayOffRepository DayOffRepository { get; }

        public IEmployeeDayOffRepository EmployeeDayOffRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public IEmployeeStatusRepository EmployeeStatusRepository { get; }

        public UnitOfWork(
            IAppointmentRepository appointmentRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IConnectionFactory connectionFactory,
            IBarberRepository barberRepository,
            IBranchRepository branchRepository,
            IDayOffRepository dayOffRepository,
            IEmployeeDayOffRepository employeeDayOffRepository,
            IEmployeeRepository employeeRepository,
            IEmployeeStatusRepository employeeStatusRepository)
        {
            factoryConnection = connectionFactory;
            connection = factoryConnection.Connect;
            transaction = connection.BeginTransaction();

            AppointmentRepository = appointmentRepository;
            CustomerRepository = customerRepository;
            UserRepository = userRepository;
            BarberRepository = barberRepository;
            BranchRepository = branchRepository;
            DayOffRepository = dayOffRepository;
            EmployeeDayOffRepository = employeeDayOffRepository;
            EmployeeRepository = employeeRepository;
            EmployeeStatusRepository = employeeStatusRepository;
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                
            }
        }
        public void Dispose()
        {
            factoryConnection.Dispose();
            transaction.Dispose();
            connection.Dispose();
        }
    }
}
