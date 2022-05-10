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
using Customers_BLL.Services.Abstract;

namespace Customers_BLL.Services.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            appointmentRepository = unitOfWork.AppointmentRepository;
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAllAsync()
        {
            var results = await appointmentRepository.GetAllAsync();
            return results.Select(mapper.Map<Appointment, AppointmentResponse>);
        }
        
        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var result = await appointmentRepository.GetByIdAsync(id);
            return mapper.Map<Appointment, AppointmentResponse>(result);
        }

        public async Task<AppointmentResponse> GetCompleteEntityAsync(int id)
        {
            var result = await appointmentRepository.GetCompleteEntityAsync(id);
            return mapper.Map<Appointment, AppointmentResponse>(result);
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAppointments(string dateStr, int barberId)
        {
          var date = DateTime.Parse(dateStr);
          var result = await appointmentRepository.GetAppointments(date, barberId);
          return result.Select(mapper.Map<Appointment, AppointmentResponse>);
        }

        public async Task InsertAsync(AppointmentRequest request)
        {
            var entity = mapper.Map<AppointmentRequest, Appointment>(request);
            await appointmentRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppointmentRequest request)
        {
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
