using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Exceptions;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;
using IdentityServer.Helpers;

namespace HR_BLL.Services.Concrete
{
    public class BarberService : IBarberService
    {
        private readonly IMapper mapper;

        private readonly IBarberRepository barberRepository;
        
        private readonly IEmployeeRepository employeeRepository;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            barberRepository = unitOfWork.BarberRepository;
            employeeRepository = unitOfWork.EmployeeRepository;
        }

        private async Task<BarberResponse> ExtendBarber(Barber barber)
        {
            var response = mapper.Map<Barber, BarberResponse>(barber);
            var employee = await employeeRepository.GetByIdAsync(barber.EmployeeUserId);
            var user = new User();
            throw new NotImplementedException("Implement getting User by id using gRPC");
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
            throw new NotImplementedException("Implement using gRPC");
            var appointments = new List<Appointment>(); // Request appointments from CustomersAPI using gRPC
            var customerIds = appointments.Select(appointment => appointment.CustomerUserId);
            var users = new List<User>(); // Request users with customerIds from IdentityAPI using gRPC
            var responses = new List<BarbersAppointmentResponse>();
            for (var index = 0; index < appointments.Count(); index++)
            {
                var appointment = appointments[index];
                var relatedUser = users[index];
                var response = mapper.Map<Appointment, BarbersAppointmentResponse>(appointment);
                response.CustomerName = $"{relatedUser.FirstName} {relatedUser.LastName}";
                responses.Add(response);
            }

            return responses;
        }
        
        public async Task<IEnumerable<BarberResponse>> GetByBranchIdAsync(int branchId)
        {
            var barbers = await barberRepository.GetByBranchIdAsync(branchId);
            var responses = new List<BarberResponse>();
            foreach (var barber in barbers)
            {
                var extendedBarber = await ExtendBarber(barber);
                responses.Add(extendedBarber);
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
