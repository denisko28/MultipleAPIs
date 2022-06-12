namespace HR_BLL.DTO.Responses
{
    public class BarberResponse
    {
        public int EmployeeUserId { get; set; }
        
        public int ChairNum { get; set; }
        
        public int BranchId { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Avatar { get; set; }
    }
}
