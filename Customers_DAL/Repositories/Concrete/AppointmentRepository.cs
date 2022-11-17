using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }
        
        public override async Task<Appointment> GetByIdAsync(int id)
        {
            var appointment = await Table.SingleOrDefaultAsync(appointment => appointment.Id == id);
            
            return appointment ?? throw new EntityNotFoundException(nameof(Appointment), id);
        }
        
        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }
        
        public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date)
        {
            var appointments = await Table
                .Where(appointment => appointment.AppDate.Equals(date))
                .ToListAsync();

            return appointments;
        }
        
        public async Task<IEnumerable<Appointment>> GetByBranchAsync(int branchId)
        {
            var appointments = await Table
                .Where(appointment => appointment.BranchId == branchId)
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetByDateAndBarberAsync(DateTime date, int barberId)
        {
            var appointments = await Table
                .Where(appointment => appointment.AppDate.Equals(date) && appointment.BarberUserId == barberId)
                .ToListAsync();

            return appointments;
        }
        
        public async Task<IEnumerable<Appointment>> GetBarbersAppointmentsAsync(int barberId)
        {
            var appointments = await Table
                .Where(appointment => appointment.BarberUserId == barberId)
                .ToListAsync();

            return appointments;
        }
        
        public async Task<IEnumerable<Service>> GetAppointmentServicesAsync(int id)
        {
            var appointment = await Table
                .Include(appointment => appointment.AppointmentServices)
                    .ThenInclude(appointmentServices => appointmentServices.Service)
                .SingleOrDefaultAsync(appointment => appointment.Id == id);

            if(appointment == null)
                throw new EntityNotFoundException(nameof(Appointment), id);

            return appointment.AppointmentServices.Select(appointmentServices => appointmentServices.Service)!;
        }
    }
}
