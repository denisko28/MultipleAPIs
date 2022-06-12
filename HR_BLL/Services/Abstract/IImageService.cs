using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_BLL.Services.Abstract
{
    public interface IImageService
    {
        Task<FileResult> GetPrivateImageAsync(string path);
        
        Task<string> SavePrivateImageAsync(IFormFile? photo, string path);
    }
}
