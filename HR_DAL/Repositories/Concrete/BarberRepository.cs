﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class BarberRepository : GenericRepository<Barber>, IBarberRepository
    {
        public BarberRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }
        
        public override async Task<Barber> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Barber WHERE EmployeeUserId=@Id";
            var values = new { Id = id };
            return await Connection.QuerySingleAsync<Barber>(sql, values);
        }

        //public async Task<IEnumerable<BarbersAppointmentsResponse>> GetBarbersAppointmentsAsync(BarbersAppointmentsRequest request)
        //{
        //    //var sql = "EXEC [GetAllProc] @Table_Name";

        //    var sql = "SELECT Appointment.Id AS 'AppointmentId', User_.FirstName, User_.LastName, AppointmentStatus.Descript AS 'AppointmentStatus', BeginTime, EndTime " +
        //        "FROM Appointment " +
        //        "INNER JOIN Customer ON CustomerId = Customer.Id " +
        //        "INNER JOIN User_ ON Customer.UserId = User_.Id " +
        //        "INNER JOIN AppointmentStatus ON Appointment.AppointmentStatusId = AppointmentStatus.Id " +
        //        "WHERE BarberId = @BarberId AND AppDate >= CONVERT(date, @_Date)";
        //    var values = new { BarberId = request.BarberId, _Date = request._Date };
        //    IEnumerable<BarbersAppointmentsResponse> results = await connection.Connect.QueryAsync<BarbersAppointmentsResponse>(sql, values);
        //    return results;
        //}

        public async Task<IEnumerable<Barber>> GetByBranchIdAsync(int branchId)
        {
            const string sql = "SELECT * FROM Barber " + 
                               "INNER JOIN Employee ON EmployeeUserId=UserId " +
                               "WHERE BranchId=@BranchId";
            var values = new { BranchId = branchId };
            return await Connection.QueryAsync<Barber>(sql, values);
        }
    }
}
