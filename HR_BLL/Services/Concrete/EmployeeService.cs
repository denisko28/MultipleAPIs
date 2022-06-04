using System.Collections.Generic;
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

        public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
        {
            var employees = await employeeRepository.GetAllAsync();
            var responses = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                var response = mapper.Map<Employee, EmployeeResponse>(employee);
                var user = await userRepository.GetByIdAsync(employee.UserId);
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Avatar = user.Avatar;
                responses.Add(response);
            }

            return responses;
        }

        public async Task<EmployeeResponse> GetByIdAsync(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            var response = mapper.Map<Employee, EmployeeResponse>(employee);
            var user = await userRepository.GetByIdAsync(employee.UserId);
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Avatar = user.Avatar;
            return response;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status)
        {
            var statusCode = EmployeeStatusHelper.GetIntStatus(status);
            var employees = await employeeRepository.GetByStatusIdAsync(statusCode);
            var responses = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                var response = mapper.Map<Employee, EmployeeResponse>(employee);
                var user = await userRepository.GetByIdAsync(employee.UserId);
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Avatar = user.Avatar;
                responses.Add(response);
            }

            return responses;
        }

        public async Task<int> InsertAsync(EmployeeRequest request)
        {
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var insertedId = await employeeRepository.InsertAsync(entity);
            return insertedId;
        }

        public async Task<bool> UpdateAsync(EmployeeRequest request)
        {
            var entity = mapper.Map<EmployeeRequest, Employee>(request);
            var result = await employeeRepository.UpdateAsync(entity);
            return result;
        }
        
        public async Task SetPassportForEmployeeAsync(ImageUploadRequest request)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id);
            employee.PassportImgPath = await imageService.SaveImageAsync(request.Image);
            await employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await employeeRepository.DeleteByIdAsync(id);
        }
    }
}
