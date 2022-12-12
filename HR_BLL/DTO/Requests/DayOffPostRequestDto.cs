using System;

namespace HR_BLL.DTO.Requests
{
    public class DayOffPostRequestDto
    {
        public int Id { get; set; }
        
        public int EmployeeUserId { get; set; }
        
        public DateTime Date { get; set; }
    }
}
