using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HR_BLL.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_BLL.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment environment;

        private static readonly Dictionary<string, string> ImageTypes = new()
        { 
            {".pdf", "application/pdf"},
            {".png", "image/png"},  
            {".jpg", "image/jpeg"},  
            {".jpeg", "image/jpeg"},  
            {".gif", "image/gif"},  
            {".csv", "text/csv"}
        };

        public ImageService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        
        private static string GetContentType(string path)  
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();  
            return ImageTypes[ext];  
        }

        public async Task<FileResult> GetPrivateImageAsync(string path)
        {
            var physicalPath = $"{Directory.GetCurrentDirectory()}/SecureStaticFiles/{path}";
            
            if (!File.Exists(physicalPath))
                throw new FileNotFoundException($"File '{path}' wasn't found");
            
            var memory = new MemoryStream();
            await using var fileStream = new FileStream(physicalPath, FileMode.Open);
            
            await fileStream.CopyToAsync(memory);  
            await fileStream.FlushAsync();
            
            memory.Position = 0;  
            return new FileStreamResult(memory, GetContentType(physicalPath));  
        }

        public async Task<string> SavePrivateImageAsync(IFormFile? photo, string path)
        {
            string imagesFolderPath = $"SecureStaticFiles/{path}";

            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}/{imagesFolderPath}/"))
            {
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/{imagesFolderPath}/");
            }

            var fileExtension = Path.GetExtension(photo?.FileName);
            string newFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{fileExtension}";
            await using var fileStream = File.Create($"{Directory.GetCurrentDirectory()}/{imagesFolderPath}/{newFileName}");

            await photo?.CopyToAsync(fileStream)!;
            await fileStream.FlushAsync();

            return newFileName;
        }
    }
}
