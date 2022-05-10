using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Customers_DAL.Repositories.Concrete
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }
        
        public override async Task<Appointment> GetByIdAsync(int id)
        {
            var appointment = await Table
                .Include(appointment => appointment.Barber)
                    .ThenInclude(barber => barber!.Employee)
                        .ThenInclude(employee => employee!.User)
                .SingleOrDefaultAsync(appointment => appointment.Id == id);
            
            return appointment ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
        
        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await Table
                .Include(appointment => appointment.Barber)
                    .ThenInclude(barber => barber!.Employee)
                        .ThenInclude(employee => employee!.User)
                .ToListAsync();
        }

        public override async Task<Appointment> GetCompleteEntityAsync(int id)
        {
            var appointment = await Table
                .Include(appointment => appointment.Barber)
                    .ThenInclude(barber => barber!.Employee)
                        .ThenInclude(employee => employee!.User)
                .Include(appointment => appointment.Barber)
                    .ThenInclude(barber => barber!.Employee)
                        .ThenInclude(employee => employee!.Branch)
                .Include(appointment => appointment.Customer)
                        .ThenInclude(employee => employee!.User)
                .Include(appointment => appointment.AppointmentServices)
                .SingleOrDefaultAsync(appointment => appointment.Id == id);

            return appointment ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(DateTime date, int barberId)
        {
            var appointments = await Table
                .Include(appointment => appointment.Barber)
                    .ThenInclude(barber => barber!.Employee)
                        .ThenInclude(employee => employee!.User)
                .Include(appointment => appointment.Customer)
                    .ThenInclude(employee => employee!.User)
                .Include(appointment => appointment.AppointmentServices)
                .Where(appointment => appointment.AppDate == date && appointment.BarberId == barberId)
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<Service>> GetAppointmentServices(int id)
        {
            var appointment = await Table
                .Include(appointment => appointment.AppointmentServices)
                    .ThenInclude(appointmentServices => appointmentServices.Service)
                .SingleOrDefaultAsync(appointment => appointment.Id == id);

            if(appointment == null)
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));

            return appointment.AppointmentServices.Select(appointmentServices => appointmentServices.Service)!;
        }
    }
}
