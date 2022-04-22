using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Helpers;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_BLL.Services.Concrete
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

        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var result = await employeeRepository.GetByIdAsync(id);
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

        public async Task DeleteByIdAsync(int id)
        {
            await employeeRepository.DeleteByIdAsync(id);
            unitOfWork.Commit();
        }
    }
}
