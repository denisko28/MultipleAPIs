using System;

namespace MultipleAPIs.HR_BLL.DTO.Responses
{
    public class EmployeeResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }

        public string EmployeeStatus { get; set; }

        public string? Adress { get; set; }

        public string? PassportImgPath { get; set; }

        public DateTime Birthday { get; set; }
    }
}
