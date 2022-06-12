namespace Customers_BLL.DTO.Requests
{
    public class BarberRegisterRequest : EmployeeRegisterRequest
    {
        public int ChairNum { get; set; }
    }
}