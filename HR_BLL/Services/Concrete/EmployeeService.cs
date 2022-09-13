using System;
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
using Microsoft.AspNetCore.Mvc;

namespace HR_BLL.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;

        private readonly IEmployeeRepository employeeRepository;
        
        private readonly IUserRepository userRepository;

        private readonly IImageService imageService;
        
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService) 
        {
            this.mapper = mapper;
            employeeRepository = unitOfWork.EmployeeRepository;
            userRepository = unitOfWork.UserRepository;
            this.imageService = imageService;
        }

        private async Task<EmployeeResponse> ExtendEmployee(Employee employee)
        {
            var response = mapper.Map<Employee, EmployeeResponse>(employee);
            var user = await userRepository.GetByIdAsync(employee.UserId);
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Avatar = user.Avatar;
            return response;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
        {
            var employees = await employeeRepository.GetAllAsync();
            var responses = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                var extendedEmployee = await ExtendEmployee(employee);
                responses.Add(extendedEmployee);
            }

            return responses;
        }
        
        public async Task<IEnumerable<EmployeeResponse>> GetAllForManager(int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employees = await employeeRepository.GetByBranchId(manager.BranchId);
            
            var responses = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                var extendedEmployee = await ExtendEmployee(employee);
                responses.Add(extendedEmployee);
            }

            return responses;
        }

        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            var extendedEmployee = await ExtendEmployee(employee);
            return extendedEmployee;
        }
        
        public async Task<EmployeeResponse> GetByIdForManager(int id, int userId)
        {
            var manager = await employeeRepository.GetByIdAsync(userId);
            var employee = await employeeRepository.GetByIdAsync(id);
            
            if(manager.BranchId != employee.BranchId)
                throw new ForbiddenAccessException(
                $"You don't have access to the employee with id: {id}");
            
            var extendedEmployee = await ExtendEmployee(employee);
            return extendedEmployee;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(int statusCode)
        {
            var employees = await employeeRepository.GetByStatusIdAsync(statusCode);
            var responses = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                var extendedEmployee = await ExtendEmployee(employee);
                responses.Add(extendedEmployee);
            }

            return responses;
        }
        
        public async Task<FileResult> GetPassportForEmployeeAsync(int employeeId, UserClaimsModel userClaims)
        {
            var employee = await employeeRepository.GetByIdAsync(employeeId);
            if (userClaims.Role == UserRoles.Manager)
            {
                var manager = await employeeRepository.GetByIdAsync(userClaims.UserId);
                if(manager.BranchId != employee.BranchId)
                    throw new ForbiddenAccessException(
                        $"You don't have access to the passport of the employee with id: {employeeId}");
            }
            
            var imgPath = "Images/Passports/" + employee.PassportImgPath;
            if (string.IsNullOrEmpty(imgPath))
                throw new Exception("Employee has no passport image attached");
            
            return await imageService.GetPrivateImageAsync(imgPath);
        }

        public async Task<int> InsertAsync(EmployeeRequest request)
        {
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var insertedId = await employeeRepository.InsertAsync(entity);
            return insertedId;
        }

        public async Task<bool> UpdateAsync(EmployeeRequest request, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != request.UserId)
                throw new ForbiddenAccessException(
                    $"You don't have access to edit employee with id: {request.UserId}");
            
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var result = await employeeRepository.UpdateAsync(entity);
            return result;
        }
        
        public async Task SetPassportForEmployeeAsync(ImageUploadRequest request, UserClaimsModel userClaims)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id);
            if (userClaims.Role == UserRoles.Manager)
            {
                var manager = await employeeRepository.GetByIdAsync(userClaims.UserId);
                if(manager.BranchId != employee.BranchId)
                    throw new ForbiddenAccessException(
                        $"You don't have access to edit passport of the employee with id: {request.Id}");
            }
            
            employee.PassportImgPath = await imageService.SavePrivateImageAsync(request.Image, "Images/Passports");
            await employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await employeeRepository.DeleteByIdAsync(id);
        }
    }
}
