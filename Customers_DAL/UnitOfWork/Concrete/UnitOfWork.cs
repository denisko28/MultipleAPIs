using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Identity;

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

        public IUserRepository UserRepository { get; }
        
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public UnitOfWork(BarbershopDbContext barbershopDbContext, IAppointmentRepository appointmentRepository,
          IPossibleTimeRepository possibleTimeRepository, IBarberRepository barberRepository,
          ICustomerRepository customerRepository, IAppointmentServiceRepository appointmentServiceRepository,
          IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.barbershopDbContext = barbershopDbContext;
            AppointmentRepository = appointmentRepository;
            AppointmentServiceRepository = appointmentServiceRepository;
            PossibleTimeRepository = possibleTimeRepository;
            BarberRepository = barberRepository;
            CustomerRepository = customerRepository;
            UserRepository = userRepository;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task SaveChangesAsync()
        {
            await barbershopDbContext.SaveChangesAsync();
        }
    }
}
