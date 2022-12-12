using Microsoft.AspNetCore.Http;

namespace HR_BLL.DTO.Requests
{
    public class ImageUploadRequestDto
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; } = null!;
    }
}
