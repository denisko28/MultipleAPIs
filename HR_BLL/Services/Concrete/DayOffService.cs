using System;
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
    public class DayOffService : IDayOffService
    {
        private readonly IMapper mapper;

        private readonly IDayOffRepository dayOffRepository;

        private readonly IEmployeeDayOffRepository employeeDayOffRepository;

        public DayOffService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            dayOffRepository = unitOfWork.DayOffRepository;
            employeeDayOffRepository = unitOfWork.EmployeeDayOffRepository;
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetAllAsync()
        {
            var results = await dayOffRepository.GetAllAsync();
            return results.Select(mapper.Map<DayOff, DayOffResponse>);
        }

        public async Task<DayOffResponse> GetByIdAsync(int id)
        {
            var result = await dayOffRepository.GetByIdAsync(id);
            return mapper.Map<DayOff, DayOffResponse>(result);
        }
        
        public async Task<DayOffResponse> GetCompleteEntity(int id)
        {
            var result = await employeeDayOffRepository.GetCompleteEntityByDayOff(id);
            return mapper.Map<object, DayOffResponse>(result);
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetAllCompleteEntities()
        {
            var results = await employeeDayOffRepository.GetAllCompleteEntities();
            return results.Select(mapper.Map<object, DayOffResponse>);
        }

        public async Task<IEnumerable<DayOffResponse>> GetDayOffsByEmployee(int employeeUserId)
        {
            var results = await employeeDayOffRepository.GetDayOffsByEmployee(employeeUserId);
            return results.Select(mapper.Map<DayOff, DayOffResponse>);
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetCompleteEntitiesByDate(DateTime date)
        {
            var results = await employeeDayOffRepository.GetCompleteEntitiesByDate(date);
            return results.Select(mapper.Map<object, DayOffResponse>);
        }

        public async Task<int> InsertAsync(DayOffPostRequest request)
        {
            var entity = mapper.Map<DayOffPostRequest, DayOff>(request);
            var insertedId = await dayOffRepository.InsertAsync(entity);
            await employeeDayOffRepository.InsertAsync(new EmployeeDayOff
            {
                EmployeeUserId = request.EmployeeUserId, 
                DayOffId = insertedId
            });
            return insertedId;
        }

        public async Task<bool> UpdateAsync(DayOffRequest request)
        {
            var entity = mapper.Map<DayOffRequest, DayOff>(request);
            var result = await dayOffRepository.UpdateAsync(entity);
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await dayOffRepository.DeleteByIdAsync(id);
        }
    }
}
