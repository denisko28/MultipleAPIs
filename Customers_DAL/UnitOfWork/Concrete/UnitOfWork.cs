using System.Threading.Tasks;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;

namespace Customers_DAL.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarbershopDbContext barbershopDbContext;

        public IAppointmentRepository AppointmentRepository { get; }

        public IPossibleTimeRepository PossibleTimeRepository { get; }

        public IBarberRepository BarberRepository { get; }

        public UnitOfWork(BarbershopDbContext barbershopDbContext, IAppointmentRepository appointmentRepository,
          IPossibleTimeRepository possibleTimeRepository, IBarberRepository barberRepository)
        {
            this.barbershopDbContext = barbershopDbContext;
            AppointmentRepository = appointmentRepository;
            PossibleTimeRepository = possibleTimeRepository;
            BarberRepository = barberRepository;
        }

        public async Task SaveChangesAsync()
        {
            await barbershopDbContext.SaveChangesAsync();
        }
    }
}
