﻿namespace HR_BLL.DTO.Requests
{
    public class EmployeeDayOffRequest
    {
        public int Id { get; set; }

        public int EmployeeUserId { get; set; }

        public int DayOffId { get; set; }
    }
}
