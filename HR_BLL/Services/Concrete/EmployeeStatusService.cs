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

        public async Task<EmployeeStatusResponse> GetByIdAsync(int id)
        {
            var result = await employeeStatusRepository.GetByIdAsync(id);
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

        public async Task DeleteByIdAsync(int id)
        {
            await employeeStatusRepository.DeleteByIdAsync(id);
            unitOfWork.Commit();
        }
    }
}
