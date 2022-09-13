using System;
using System.IO;
using System.Threading.Tasks;
using Customers_BLL.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Customers_BLL.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment environment;

        public ImageService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile? photo)
        {
            const string imagesFolderPath = "Public/Images/Avatars";

            if (!Directory.Exists($"{environment.WebRootPath}/{imagesFolderPath}/"))
            {
                Directory.CreateDirectory($"{environment.WebRootPath}/{imagesFolderPath}/");
            }

            var fileExtension = Path.GetExtension(photo?.FileName);
            string newFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{fileExtension}";
            await using var fileStream = File.Create($"{environment.WebRootPath}/{imagesFolderPath}/{newFileName}");

            await photo?.CopyToAsync(fileStream)!;
            await fileStream.FlushAsync();

            return $"{imagesFolderPath}/{newFileName}";
        }
    }
}
