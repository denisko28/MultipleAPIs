using System.Threading.Tasks;
using Customers_BLL.Helpers;

namespace Customers_BLL.Services.Abstract
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageModel message);
    }
}