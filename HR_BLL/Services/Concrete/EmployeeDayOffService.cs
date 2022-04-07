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
    public class EmployeeDayOffService : IEmployeeDayOffService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IEmployeeDayOffRepository employeeDayOffRepository;

        public EmployeeDayOffService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            employeeDayOffRepository = unitOfWork.EmployeeDayOffRepository;
        }

        public async Task<IEnumerable<EmployeeDayOffResponse>> GetAllAsync()
        {
            var results = await employeeDayOffRepository.GetAllAsync();
            return results.Select(mapper.Map<EmployeeDayOff, EmployeeDayOffResponse>);
        }

        public async Task<EmployeeDayOffResponse> GetByIdAsync(int Id)
        {
            var result = await employeeDayOffRepository.GetByIdAsync(Id);
            return mapper.Map<EmployeeDayOff, EmployeeDayOffResponse>(result);
        }

        public async Task<int> InsertAsync(EmployeeDayOffRequest request)
        {
            var entity = mapper.Map<EmployeeDayOffRequest, EmployeeDayOff>(request);
            var result = await employeeDayOffRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(EmployeeDayOffRequest request)
        {
            var entity = mapper.Map<EmployeeDayOffRequest, EmployeeDayOff>(request);
            var result = await employeeDayOffRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            await employeeDayOffRepository.DeleteByIdAsync(Id);
            unitOfWork.Commit();
        }
    }
}
