using Microsoft.AspNetCore.Http;

namespace InternConnect.Context.Models
{
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }
    }
}