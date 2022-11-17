using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;

namespace Customers_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarbershopDbContext _barbershopDbContext;

        public IAppointmentRepository AppointmentRepository { get; }

        public IAppointmentServiceRepository AppointmentServiceRepository { get; set; }

        public IPossibleTimeRepository PossibleTimeRepository { get; }

        public ICustomerRepository CustomerRepository { get; }
        
        public IBranchRepository BranchRepository { get; }
        
        public IServiceRepository ServiceRepository { get; }

        public UnitOfWork(BarbershopDbContext barbershopDbContext, IAppointmentRepository appointmentRepository,
            IPossibleTimeRepository possibleTimeRepository,
            ICustomerRepository customerRepository, IAppointmentServiceRepository appointmentServiceRepository,
            IBranchRepository branchRepository, IServiceRepository serviceRepository)
        {
            this._barbershopDbContext = barbershopDbContext;
            AppointmentRepository = appointmentRepository;
            AppointmentServiceRepository = appointmentServiceRepository;
            PossibleTimeRepository = possibleTimeRepository;
            CustomerRepository = customerRepository;
            BranchRepository = branchRepository;
            ServiceRepository = serviceRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _barbershopDbContext.SaveChangesAsync();
        }
    }
}