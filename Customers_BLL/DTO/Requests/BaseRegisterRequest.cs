namespace Customers_BLL.DTO.Requests
{
    public class BaseRegisterRequest
    {
        public string Email { get; set; } = null!;
        
        public string Password { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; } = null!;
    }
}