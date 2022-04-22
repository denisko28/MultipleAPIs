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

        public async Task<EmployeeDayOffResponse> GetByIdAsync(int id)
        {
            var result = await employeeDayOffRepository.GetByIdAsync(id);
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

        public async Task DeleteByIdAsync(int id)
        {
            await employeeDayOffRepository.DeleteByIdAsync(id);
            unitOfWork.Commit();
        }
    }
}
