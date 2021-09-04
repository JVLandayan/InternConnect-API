using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Service.ThirdParty
{
    public interface IUploadService
    {
        public ActionResult<string> UploadImage(string entity, ControllerBase controller, FileUploadAPI uploadedFile);
        public ActionResult<string> SubmissionFiles(ControllerBase controller, FileUploadAPI uploadedFile);
    }

    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadService(IWebHostEnvironment iWebHostEnvironment)
        {
            _webHostEnvironment = iWebHostEnvironment;
        }

        public ActionResult<string> SubmissionFiles(ControllerBase controller, FileUploadAPI uploadedFile)
        {
            try
            {
                if (uploadedFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.ContentRootPath + "\\files\\";
                    string fileExtension = Path.GetExtension(uploadedFile.files.FileName);
                    string fileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + Guid.NewGuid().ToString() + fileExtension;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(Path.Combine(path, fileName)))
                    {
                        uploadedFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return fileName;
                    }
                }
                return "uploading error";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public ActionResult<string> UploadImage(string entity, ControllerBase controller, FileUploadAPI uploadedFile)
        {
            try
            {
                if (uploadedFile.files.Length > 0)
                {
                    string path;
                    string fileExtension = Path.GetExtension(uploadedFile.files.FileName);
                    string fileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + Guid.NewGuid().ToString() + fileExtension;
                    if (entity == "company")
                    {
                        path = _webHostEnvironment.ContentRootPath + "\\images\\company\\";
                    }
                    else if (entity == "logo")
                    {
                        path = _webHostEnvironment.ContentRootPath + "\\images\\logo\\";
                    }
                    else
                    {
                        path = _webHostEnvironment.ContentRootPath + "\\images\\signatures\\";
                    }
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(Path.Combine(path, fileName)))
                    {
                        uploadedFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return fileName;
                    }
                }
                return "Uploading Error";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }


            


        }
    }
}
