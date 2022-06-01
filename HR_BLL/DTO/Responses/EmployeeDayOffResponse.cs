using System;

namespace HR_BLL.DTO.Responses
{
    public class EmployeeDayOffResponse
    {
        public int Id { get; set; }

        public int EmployeeUserId { get; set; }

        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }

        public int DayOffId { get; set; }
        
        public DateTime? Date_ { get; set; }
    }
}
