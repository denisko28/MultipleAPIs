using System;

namespace Customers_BLL.DTO.Requests
{
    public class EmployeeRegisterRequest : BaseRegisterRequest
    {
        public int BranchId { get; set; }

        public string EmployeeStatus { get; set; } = null!;
        
        public string Address { get; set; } = null!;
        
        public string? PassportImgPath { get; set; }
        
        public DateTime Birthday { get; set; }
    }
}