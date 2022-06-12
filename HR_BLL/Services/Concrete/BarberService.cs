using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Exceptions;
using HR_BLL.Helpers;
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

        private readonly IEmployeeRepository employeeRepository;
        
        private readonly ICustomerRepository customerRepository;

        private readonly IUserRepository userRepository;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            barberRepository = unitOfWork.BarberRepository;
            appointmentRepository = unitOfWork.AppointmentRepository;
            employeeRepository = unitOfWork.EmployeeRepository;
            customerRepository = unitOfWork.CustomerRepository;
            userRepository = unitOfWork.UserRepository;
        }

        private async Task<BarberResponse> ExtendBarber(Barber barber)
        {
            var response = mapper.Map<Barber, BarberResponse>(barber);
            var employee = await employeeRepository.GetByIdAsync(barber.EmployeeUserId);
            var user = await userRepository.GetByIdAsync(barber.EmployeeUserId);
            response.BranchId = employee.BranchId;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Avatar = user.Avatar;
            return response;
        }

        public async Task<IEnumerable<BarberResponse>> GetAllAsync()
        {
            var barbers = await barberRepository.GetAllAsync();
            var responses = new List<BarberResponse>();
            foreach (var barber in barbers)
            {
                var extendedBarber = await ExtendBarber(barber);
                responses.Add(extendedBarber);
            }

            return responses;
        }

        public async Task<BarberResponse> GetByIdAsync(int id)
        {
            var barber = await barberRepository.GetByIdAsync(id);
            var extendedBarber = await ExtendBarber(barber);
            return extendedBarber;
        }

        public async Task<IEnumerable<BarbersAppointmentResponse>> GetBarbersAppointmentsAsync(int barberId, string dateStr, UserClaimsModel userClaims)
        {
            var exception = new ForbiddenAccessException(
                $"You don't have access to appointments of the barber with id: {barberId}");
            switch (userClaims.Role)
            {
                case UserRoles.Manager:
                {
                    var manager = await employeeRepository.GetByIdAsync(userClaims.UserId);
                    var barber = await employeeRepository.GetByIdAsync(userClaims.UserId);
                    if (manager.BranchId != barber.BranchId)
                        throw exception;
                    break;
                }
                case UserRoles.Barber when userClaims.UserId != barberId:
                    throw exception;
            }

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

        public async Task<bool> UpdateAsync(BarberRequest request, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != request.EmployeeUserId)
                throw new ForbiddenAccessException(
                    $"You don't have access to edit barber with id: {request.EmployeeUserId}");
            
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
