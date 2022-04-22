﻿using Microsoft.AspNetCore.Http;

namespace HR_BLL.DTO.Requests
{
    public class ImageUploadRequest
    {
        public int Id { get; set; }

        public IFormFile? Image { get; set; }
    }
}
