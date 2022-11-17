using System;
using System.Collections.Generic;
using System.Linq;
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
using IdentityServer.Helpers;

namespace HR_BLL.Services.Concrete
{
    public class DayOffService : IDayOffService
    {
        private readonly IMapper mapper;

        private readonly IEmployeeRepository employeeRepository;

        private readonly IDayOffRepository dayOffRepository;

        private readonly IEmployeeDayOffRepository employeeDayOffRepository;

        public DayOffService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            employeeRepository = unitOfWork.EmployeeRepository;
            dayOffRepository = unitOfWork.DayOffRepository;
            employeeDayOffRepository = unitOfWork.EmployeeDayOffRepository;
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetAllAsync()
        {
            var results = await employeeDayOffRepository.GetAllCompleteEntities();
            return results.Select(mapper.Map<object, DayOffResponse>);
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetAllForManager(int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            
            var results = await employeeDayOffRepository.GetCompleteEntitiesByBranchId(manager.BranchId);
            return results.Select(mapper.Map<object, DayOffResponse>);
        }
        
        public async Task<DayOffResponse> GetByIdAsync(int id)
        {
            var result = await employeeDayOffRepository.GetCompleteEntityByDayOff(id);
            return mapper.Map<object, DayOffResponse>(result);
        }
        
        public async Task<DayOffResponse> GetByIdForManager(int id, int userId)
        {
            var result = await GetByIdAsync(id);

            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(result.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day off with id: {id}");

            return result;
        }

        public async Task<IEnumerable<DayOffResponse>> GetDayOffsByEmployee(int employeeUserId, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != employeeUserId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day offs of the employee with id: {employeeUserId}");
            
            var results = await employeeDayOffRepository.GetDayOffsByEmployee(employeeUserId);
            return results.Select(mapper.Map<DayOff, DayOffResponse>);
        }
        
        public async Task<IEnumerable<DayOffResponse>> GetDayOffsByEmployeeForManager(int employeeUserId, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(employeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day offs of the employee with id: {employeeUserId}");
            
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
        
        public async Task<int> InsertForManagerAsync(DayOffPostRequest request, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(request.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to add day off for the employee with id: {request.EmployeeUserId}");
            
            return await InsertAsync(request);
        }

        public async Task<bool> UpdateAsync(DayOffRequest request)
        {
            var entity = mapper.Map<DayOffRequest, DayOff>(request);
            var result = await dayOffRepository.UpdateAsync(entity);
            return result;
        }
        
        public async Task<bool> UpdateForManager(DayOffRequest request, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var dayOff = await GetByIdAsync(request.Id);
            var employee = await employeeRepository.GetByIdAsync(dayOff.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to edit day off with id: {request.Id}");

            return await UpdateAsync(request);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await dayOffRepository.DeleteByIdAsync(id);
        }
        
        public async Task DeleteByIdForManager(int id, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var dayOff = await GetByIdAsync(id);
            var employee = await employeeRepository.GetByIdAsync(dayOff.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to delete day off with id: {id}");
            
            await DeleteByIdAsync(id);
        }
    }
}
