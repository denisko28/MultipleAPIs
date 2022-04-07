using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MultipleAPIs.HR_DAL.UnitOfWorks.Abstract;
using MultipleAPIs.HR_BLL.DTO.Requests;
using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using MultipleAPIs.HR_BLL.Services.Abstract;
using MultipleAPIs.HR_BLL.Helpers;

namespace MultipleAPIs.HR_BLL.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            employeeRepository = unitOfWork.EmployeeRepository;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
        {
            var results = await employeeRepository.GetAllAsync();
            return results.Select(mapper.Map<Employee, EmployeeResponse>);
        }

        public async Task<EmployeeResponse> GetByIdAsync(int Id)
        {
            var result = await employeeRepository.GetByIdAsync(Id);
            return mapper.Map<Employee, EmployeeResponse>(result);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status)
        {
            int statusCode = EmployeeStatusHelper.GetIntStatus(status);
            var result = await employeeRepository.GetByStatusIdAsync(statusCode);
            return result.Select(mapper.Map<Employee, EmployeeResponse>);
        }

        public async Task<int> InsertAsync(EmployeeRequest request)
        {
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var result = await employeeRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(EmployeeRequest request)
        {
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var result = await employeeRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            await employeeRepository.DeleteByIdAsync(Id);
            unitOfWork.Commit();
        }
    }
}
