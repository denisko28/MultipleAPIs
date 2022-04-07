using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MultipleAPIs.HR_BLL.Services.Abstract;

namespace MultipleAPIs.HR_BLL.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment environment;

        public ImageService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile photo)
        {
            const string ImagesFolderPath = "Public\\Images";

            if (!Directory.Exists($"{environment.WebRootPath}\\{ImagesFolderPath}\\"))
            {
                Directory.CreateDirectory($"{environment.WebRootPath}\\{ImagesFolderPath}\\");
            }

            string fileExtension = Path.GetExtension(photo.FileName);
            string newFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{fileExtension}";
            await using var fileStream = File.Create($"{environment.WebRootPath}\\{ImagesFolderPath}\\{newFileName}");

            photo.CopyTo(fileStream);
            await fileStream.FlushAsync();

            return newFileName;
        }
    }
}
