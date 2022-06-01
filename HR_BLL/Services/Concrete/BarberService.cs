using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_BLL.Services.Concrete
{
    public class BarberService : IBarberService
    {
        private readonly IMapper mapper;

        private readonly IBarberRepository barberRepository;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly ICustomerRepository customerRepository;

        private readonly IUserRepository userRepository;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            barberRepository = unitOfWork.BarberRepository;
            appointmentRepository = unitOfWork.AppointmentRepository;
            customerRepository = unitOfWork.CustomerRepository;
            userRepository = unitOfWork.UserRepository;
        }

        public async Task<IEnumerable<BarberResponse>> GetAllAsync()
        {
            var barbers = await barberRepository.GetAllAsync();
            var responses = new List<BarberResponse>();
            foreach (var barber in barbers)
            {
                var response = mapper.Map<Barber, BarberResponse>(barber);
                var user = await userRepository.GetByIdAsync(barber.EmployeeUserId);
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Avatar = user.Avatar;
                responses.Add(response);
            }

            return responses;
        }

        public async Task<BarberResponse> GetByIdAsync(int id)
        {
            var barber = await barberRepository.GetByIdAsync(id);
            var response = mapper.Map<Barber, BarberResponse>(barber);
            var user = await userRepository.GetByIdAsync(barber.EmployeeUserId);
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Avatar = user.Avatar;
            return response;
        }

        public async Task<IEnumerable<BarbersAppointmentResponse>> GetBarbersAppointmentsAsync(int barberId, string dateStr)
        {
            var appointments = await appointmentRepository.GetAppointments(barberId, dateStr);
            var responses = new List<BarbersAppointmentResponse>();
            foreach (var appointment in appointments)
            {
                var response = mapper.Map<Appointment, BarbersAppointmentResponse>(appointment);
                var customer = await customerRepository.GetByIdAsync(appointment.CustomerUserId);
                var user = await userRepository.GetByIdAsync(customer.UserId);
                response.CustomerName = $"{user.FirstName} {user.LastName}";
                responses.Add(response);
            }
            return responses;
        }

        public async Task<int> InsertAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var insertedId = await barberRepository.InsertAsync(entity);
            return insertedId;
        }

        public async Task<bool> UpdateAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var result = await barberRepository.UpdateAsync(entity);
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await barberRepository.DeleteByIdAsync(id);
        }
    }
}
