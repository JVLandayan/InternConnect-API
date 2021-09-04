using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternConnect.Context.Models
{
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }
    }
}
