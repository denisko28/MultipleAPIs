using AutoMapper;
using MultipleAPIs.HR_BLL.DTO.Requests;
using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using MultipleAPIs.HR_BLL.Services.Abstract;
using MultipleAPIs.HR_DAL.UnitOfWorks.Abstract;

namespace MultipleAPIs.HR_BLL.Services.Concrete
{
    public class BarberService : IBarberService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IBarberRepository barberRepository;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly ICustomerRepository customerRepository;

        private readonly IUserRepository userRepository;

        public BarberService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            barberRepository = unitOfWork.BarberRepository;
            appointmentRepository = unitOfWork.AppointmentRepository;
            customerRepository = unitOfWork.CustomerRepository;
            userRepository = unitOfWork.UserRepository;
        }

        public async Task<IEnumerable<BarberResponse>> GetAllAsync()
        {
            var results = await barberRepository.GetAllAsync();
            return results.Select(mapper.Map<Barber, BarberResponse>);
        }

        public async Task<BarberResponse> GetByIdAsync(int Id)
        {
            var result = await barberRepository.GetByIdAsync(Id);
            return mapper.Map<Barber, BarberResponse>(result);
        }

        public async Task<IEnumerable<BarbersAppointmentsResponse>> GetBarbersAppointmentsAsync(BarbersAppointmentsRequest request)
        {
            //var sql = "SELECT Appointment.Id AS 'AppointmentId', User_.FirstName, User_.LastName, AppointmentStatus.Descript AS 'AppointmentStatus', BeginTime, EndTime " +
            //    "FROM Appointment " +
            //    "INNER JOIN Customer ON CustomerId = Customer.Id " +
            //    "INNER JOIN User_ ON Customer.UserId = User_.Id " +
            //    "INNER JOIN AppointmentStatus ON Appointment.AppointmentStatusId = AppointmentStatus.Id " +
            //    "WHERE BarberId = @BarberId AND AppDate >= CONVERT(date, @_Date)";
            //var values = new { BarberId = request.BarberId, _Date = request._Date };
            //IEnumerable<BarbersAppointmentsResponse> results = await connection.Connect.QueryAsync<BarbersAppointmentsResponse>(sql, values);
            //return results;
            if (request._Date == null)
                request._Date = "";

            var appointments = await appointmentRepository.GetAppointmentsByBarberIdAndDate(request.BarberId, request._Date);
            var responses = new List<BarbersAppointmentsResponse>();
            foreach (var appointment in appointments)
            {
                var response = mapper.Map<Appointment, BarbersAppointmentsResponse>(appointment);
                var customer = await customerRepository.GetByIdAsync(appointment.CustomerId);
                var user = await userRepository.GetByIdAsync(customer.UserId);
                response.CustomerName = $"{user.FirstName} {user.LastName}";
                responses.Add(response);
            }
            return responses;
        }

        public async Task<int> InsertAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var result = await barberRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(BarberRequest request)
        {
            var entity = mapper.Map<BarberRequest, Barber>(request);
            var result = await barberRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            await barberRepository.DeleteByIdAsync(Id);
            unitOfWork.Commit();
        }
    }
}
