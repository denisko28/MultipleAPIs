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
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IBarberRepository barberRepository;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly ICustomerRepository customerRepository;

        private readonly IUserRepository userRepository;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            barberRepository = unitOfWork.BarberRepository;
            appointmentRepository = unitOfWork.AppointmentRepository;
            customerRepository = unitOfWork.CustomerRepository;
            userRepository = unitOfWork.UserRepository;
        }

        public async Task<IEnumerable<BarberResponse>> GetAllAsync()
        {
            var results = await barberRepository.GetAllAsync();
            return results.Select(mapper.Map<Barber, BarberResponse>);
        }

        public async Task<BarberResponse> GetByIdAsync(int id)
        {
            var result = await barberRepository.GetByIdAsync(id);
            return mapper.Map<Barber, BarberResponse>(result);
        }

        public async Task<IEnumerable<BarbersAppointmentsResponse>> GetBarbersAppointmentsAsync(BarbersAppointmentsRequest request)
        {
            request._Date ??= "";

            var appointments = await appointmentRepository.GetAppointmentsByBarberIdAndDate(request.BarberId, request._Date);
            var responses = new List<BarbersAppointmentsResponse>();
            foreach (var appointment in appointments)
            {
                var response = mapper.Map<Appointment, BarbersAppointmentsResponse>(appointment);
                var customer = await customerRepository.GetByIdAsync(appointment.CustomerId);
                var user = await userRepository.GetByIdAsync(customer.UserId);
                response.CustomerName = $"{user.FirstName} {user.LastName}";
                responses.Add(response);
            }
            return responses;
        }

        public async Task<int> InsertAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var result = await barberRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var result = await barberRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await barberRepository.DeleteByIdAsync(id);
            unitOfWork.Commit();
        }
    }
}
