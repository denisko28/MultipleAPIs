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
        
        public async Task<IEnumerable<DayOffResponseDto>> GetAllAsync()
        {
            var results = await employeeDayOffRepository.GetAllCompleteEntities();
            return results.Select(mapper.Map<object, DayOffResponseDto>);
        }
        
        public async Task<IEnumerable<DayOffResponseDto>> GetAllForManager(int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            
            var results = await employeeDayOffRepository.GetCompleteEntitiesByBranchId(manager.BranchId);
            return results.Select(mapper.Map<object, DayOffResponseDto>);
        }
        
        public async Task<DayOffResponseDto> GetByIdAsync(int id)
        {
            var result = await employeeDayOffRepository.GetCompleteEntityByDayOff(id);
            return mapper.Map<object, DayOffResponseDto>(result);
        }
        
        public async Task<DayOffResponseDto> GetByIdForManager(int id, int userId)
        {
            var result = await GetByIdAsync(id);

            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(result.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day off with id: {id}");

            return result;
        }

        public async Task<IEnumerable<DayOffResponseDto>> GetDayOffsByEmployee(int employeeUserId, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != employeeUserId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day offs of the employee with id: {employeeUserId}");
            
            var results = await employeeDayOffRepository.GetDayOffsByEmployee(employeeUserId);
            return results.Select(mapper.Map<DayOff, DayOffResponseDto>);
        }
        
        public async Task<IEnumerable<DayOffResponseDto>> GetDayOffsByEmployeeForManager(int employeeUserId, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(employeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to day offs of the employee with id: {employeeUserId}");
            
            var results = await employeeDayOffRepository.GetDayOffsByEmployee(employeeUserId);
            return results.Select(mapper.Map<DayOff, DayOffResponseDto>);
        }
        
        public async Task<IEnumerable<DayOffResponseDto>> GetCompleteEntitiesByDate(DateTime date)
        {
            var results = await employeeDayOffRepository.GetCompleteEntitiesByDate(date);
            return results.Select(mapper.Map<object, DayOffResponseDto>);
        }

        public async Task<int> InsertAsync(DayOffPostRequestDto requestDto)
        {
            var entity = mapper.Map<DayOffPostRequestDto, DayOff>(requestDto);
            var insertedId = await dayOffRepository.InsertAsync(entity);
            await employeeDayOffRepository.InsertAsync(new EmployeeDayOff
            {
                EmployeeUserId = requestDto.EmployeeUserId, 
                DayOffId = insertedId
            });
            return insertedId;
        }
        
        public async Task<int> InsertForManagerAsync(DayOffPostRequestDto requestDto, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(requestDto.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to add day off for the employee with id: {requestDto.EmployeeUserId}");
            
            return await InsertAsync(requestDto);
        }

        public async Task<bool> UpdateAsync(DayOffRequestDto requestDto)
        {
            var entity = mapper.Map<DayOffRequestDto, DayOff>(requestDto);
            var result = await dayOffRepository.UpdateAsync(entity);
            return result;
        }
        
        public async Task<bool> UpdateForManager(DayOffRequestDto requestDto, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var dayOff = await GetByIdAsync(requestDto.Id);
            var employee = await employeeRepository.GetByIdAsync(dayOff.EmployeeUserId);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                    $"You don't have access to edit day off with id: {requestDto.Id}");

            return await UpdateAsync(requestDto);
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
