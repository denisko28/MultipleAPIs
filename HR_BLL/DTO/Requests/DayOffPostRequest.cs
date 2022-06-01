using System;

namespace HR_BLL.DTO.Requests
{
    public class DayOffPostRequest
    {
        public int Id { get; set; }
        
        public int EmployeeUserId { get; set; }
        
        public DateTime Date { get; set; }
    }
}
