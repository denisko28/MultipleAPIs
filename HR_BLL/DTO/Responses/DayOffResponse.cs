using System;

namespace HR_BLL.DTO.Responses
{
    public class DayOffResponse
    {
        public int Id { get; set; }

        public int EmployeeUserId { get; set; }

        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public DateTime? Date_ { get; set; }
    }
}
