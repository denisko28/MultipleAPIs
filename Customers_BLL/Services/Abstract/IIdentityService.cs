using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;

namespace Customers_BLL.Services.Abstract
{
    public interface IIdentityService
    {
        Task<JwtResponse> Login(UserLoginRequest request);

        Task<string> RegisterCustomer(CustomerRegisterRequest request);
        
        Task<string> RegisterBarber(BarberRegisterRequest request);

        Task<string> RegisterManager(EmployeeRegisterRequest request);

        Task<string> ConfirmEmail(int userId, string? confirmToken);

        Task<string> ForgotPasswordAsync(string email);

        Task<string> ResetPasswordAsync(ResetPasswordRequest request);
    }
}