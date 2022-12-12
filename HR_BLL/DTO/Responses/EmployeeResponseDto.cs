using System;

namespace HR_BLL.DTO.Responses
{
    public class EmployeeResponseDto
    {
        public int UserId { get; set; }

        public int BranchId { get; set; }

        public string EmployeeStatus { get; set; } = null!;

        public string? Address { get; set; }

        public string? PassportImgPath { get; set; }

        public DateTime Birthday { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Avatar { get; set; }
    }
}
