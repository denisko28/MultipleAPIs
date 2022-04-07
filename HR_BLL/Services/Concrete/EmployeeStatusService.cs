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

namespace MultipleAPIs.HR_BLL.Services.Concrete
{
    public class EmployeeStatusService : IEmployeeStatusService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IEmployeeStatusRepository employeeStatusRepository;

        public EmployeeStatusService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            employeeStatusRepository = unitOfWork.EmployeeStatusRepository;
        }

        public async Task<IEnumerable<EmployeeStatusResponse>> GetAllAsync()
        {
            var results = await employeeStatusRepository.GetAllAsync();
            return results.Select(mapper.Map<EmployeeStatus, EmployeeStatusResponse>);
        }

        public async Task<EmployeeStatusResponse> GetByIdAsync(int Id)
        {
            var result = await employeeStatusRepository.GetByIdAsync(Id);
            return mapper.Map<EmployeeStatus, EmployeeStatusResponse>(result);
        }

        public async Task<int> InsertAsync(EmployeeStatusRequest request)
        {
            var entity = mapper.Map<EmployeeStatusRequest, EmployeeStatus>(request);
            var result = await employeeStatusRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(EmployeeStatusRequest request)
        {
            var entity = mapper.Map<EmployeeStatusRequest, EmployeeStatus>(request);
            var result = await employeeStatusRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            await employeeStatusRepository.DeleteByIdAsync(Id);
            unitOfWork.Commit();
        }
    }
}
