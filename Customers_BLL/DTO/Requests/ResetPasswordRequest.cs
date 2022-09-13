namespace Customers_BLL.DTO.Requests
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }

        public string NewPassword { get; set; } = null!;
        
        public string Token { get; set; } = null!;
    }
}