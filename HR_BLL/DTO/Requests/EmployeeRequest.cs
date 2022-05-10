using System;

namespace HR_BLL.DTO.Requests
{
    public class EmployeeRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }

        public string? EmployeeStatus { get; set; }

        public string? Address { get; set; }

        public string? PassportImgPath { get; set; }

        public DateTime Birthday { get; set; }
    }
}
