using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Services.Abstract;
using IdentityServer.Helpers;

namespace Customers_BLL.Services.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly IAppointmentServiceRepository appointmentServiceRepository;
        
        private readonly IPossibleTimeRepository possibleTimeRepository;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            appointmentRepository = unitOfWork.AppointmentRepository;
            appointmentServiceRepository = unitOfWork.AppointmentServiceRepository;
            possibleTimeRepository = unitOfWork.PossibleTimeRepository;
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAllAsync(UserClaimsModel userClaims)
        {
            return userClaims.Role switch
            {
                UserRoles.Admin => await GetAllForAdmin(),
                UserRoles.Manager => await GetAllForManager(userClaims.BranchId),
                _ => throw new ForbiddenAccessException("User role is undefined")
            };
        }

        private async Task<IEnumerable<AppointmentResponse>> GetAllForAdmin()
        {
            var results = await appointmentRepository.GetAllAsync();
            return results.Select(mapper.Map<Appointment, AppointmentResponse>);
        }
        
        private async Task<IEnumerable<AppointmentResponse>> GetAllForManager(int branchId)
        {
            var result = await appointmentRepository.GetByBranchAsync(branchId);
            return result.Select(mapper.Map<Appointment, AppointmentResponse>);
        }
        
        public async Task<AppointmentResponse> GetByIdAsync(int id, UserClaimsModel userClaims)
        {
            return userClaims.Role switch
            {
                UserRoles.Admin => await GetByIdForAdmin(id),
                UserRoles.Manager => await GetByIdForManager(id, userClaims.BranchId),
                _ => throw new ForbiddenAccessException("User role is undefined")
            };
        }
        
        private async Task<AppointmentResponse> GetByIdForAdmin(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            return mapper.Map<Appointment, AppointmentResponse>(appointment);
        }
        
        private async Task<AppointmentResponse> GetByIdForManager(int id, int branchId)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            
            if(appointment.BranchId != branchId)
                throw new ForbiddenAccessException($"You don't have access to the appointment with id: {id}");
            
            return mapper.Map<Appointment, AppointmentResponse>(appointment);
        }
        
        public async Task<IEnumerable<AppointmentResponse>> GetByDateAsync(string dateStr)
        {
            var date = DateTime.Parse(dateStr);
            var result = await appointmentRepository.GetByDateAsync(date);
            return result.Select(mapper.Map<Appointment, AppointmentResponse>);
        }

        public async Task<IEnumerable<ServiceResponse>> GetAppointmentServicesAsync(int appointmentId)
        {
            var result = await appointmentRepository.GetAppointmentServicesAsync(appointmentId);
            return result.Select(mapper.Map<Service, ServiceResponse>);
        }

        public async Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr)
        { 
            var availableTime = new List<TimeResponse>();
            var barbersDayOffs = new List<DayOff>();
            throw new NotImplementedException("Implement GetBarbersDayOffs using gRPC");

            var date = DateTime.Parse(dateStr);
            if(barbersDayOffs.Any( dayOff => dayOff.Date.Equals(date)))
              return availableTime;
            
            var possibleTime = (await possibleTimeRepository.GetAllAvailableAsync())
              .Select(possibleTime => possibleTime.Time).ToList();
            
            var barbersAppointsForDate = (List<Appointment>) await appointmentRepository.GetByDateAndBarberAsync(date, barberId);

            for (var i = 0; i < barbersAppointsForDate.Count;)
            {
              for (var j = 0; j < possibleTime.Count; j++)
              {
                if (possibleTime[j] <= barbersAppointsForDate[i].BeginTime ||
                    possibleTime[j] >= barbersAppointsForDate[i].EndTime)
                    continue;

                possibleTime.RemoveAt(j);
                j--;
              }
              barbersAppointsForDate.RemoveAt(i);
            }

            for (var i = 0; i < possibleTime.Count; i++)
            {
              var available = true;
              var stepSize = new TimeSpan(0, 15, 0);
              var steps = duration/15;
              for(var j = 0; (j < possibleTime.Count && j < steps); j++)
              {
                var expectedTime = possibleTime[i + j].Add(stepSize).ToString();
                if (possibleTime.Any(time => time.ToString() == expectedTime))
                  continue;
                available = false;
                break;
              }

              if (!available)
                continue;
              var beginTime = possibleTime[i];
              var endTime = possibleTime[i].Add(new TimeSpan(0, duration, 0));
              availableTime.Add(new TimeResponse(){ BeginTime = beginTime, EndTime = endTime });
            }

            return availableTime;
        }

        public async Task InsertAsync(AppointmentPostRequest request)
        {
            var entity = mapper.Map<AppointmentPostRequest, Appointment>(request);
            entity.AppointmentStatusId = 0;
            await appointmentRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();

            var insertedId = entity.Id;
            var appServices = request.ServiceIds
                .Select(serviceId => 
                    new Customers_DAL.Entities.AppointmentService {AppointmentId = insertedId, ServiceId = serviceId}
                ).ToList();
            await appointmentServiceRepository.InsertRangeAsync(appServices);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppointmentRequest request, UserClaimsModel userClaims)
        {
            switch (userClaims.Role)
            {
                case UserRoles.Admin:
                    await UpdateForAdminAsync(request);
                    break;
                case UserRoles.Manager:
                    await UpdateForManagerAsync(request, userClaims.BranchId);
                    break;
                case UserRoles.Barber:
                    await UpdateForBarberAsync(request, userClaims.UserId);
                    break;
            }
        }

        private async Task UpdateForAdminAsync(AppointmentRequest request)
        {
            var entity = mapper.Map<AppointmentRequest, Appointment>(request);
            await appointmentRepository.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateForManagerAsync(AppointmentRequest request, int branchId)
        {
            if (request.BranchId != branchId)
                throw new ForbiddenAccessException($"You don't have access to edit appointment with id: {request.Id}");
            
            var entity = mapper.Map<AppointmentRequest, Appointment>(request);
            await appointmentRepository.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }
        
        private async Task UpdateForBarberAsync(AppointmentRequest request, int userId)
        {
            if (request.BarberUserId != userId)
                throw new ForbiddenAccessException($"You don't have access to edit appointment with id: {request.Id}");
            
            var entity = mapper.Map<AppointmentRequest, Appointment>(request);
            await appointmentRepository.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await appointmentRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
