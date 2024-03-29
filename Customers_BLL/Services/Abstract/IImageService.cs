﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Customers_BLL.Services.Abstract
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile? photo);
    }
}
