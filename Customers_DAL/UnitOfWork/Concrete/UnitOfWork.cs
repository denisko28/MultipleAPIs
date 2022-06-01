using System.Threading.Tasks;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;

namespace Customers_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarbershopDbContext barbershopDbContext;

        public IAppointmentRepository AppointmentRepository { get; }

        public IAppointmentServiceRepository AppointmentServiceRepository { get; set; }

        public IPossibleTimeRepository PossibleTimeRepository { get; }

        public IBarberRepository BarberRepository { get; }
        
        public ICustomerRepository CustomerRepository { get; }

        public UnitOfWork(BarbershopDbContext barbershopDbContext, IAppointmentRepository appointmentRepository,
          IPossibleTimeRepository possibleTimeRepository, IBarberRepository barberRepository,
          ICustomerRepository customerRepository, IAppointmentServiceRepository appointmentServiceRepository)
        {
            this.barbershopDbContext = barbershopDbContext;
            AppointmentRepository = appointmentRepository;
            AppointmentServiceRepository = appointmentServiceRepository;
            PossibleTimeRepository = possibleTimeRepository;
            BarberRepository = barberRepository;
            CustomerRepository = customerRepository;
        }

        public async Task SaveChangesAsync()
        {
            await barbershopDbContext.SaveChangesAsync();
        }
    }
}
