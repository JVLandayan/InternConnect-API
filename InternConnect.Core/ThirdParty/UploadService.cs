using System;
using System.IO;
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
                    var path = _webHostEnvironment.ContentRootPath + "\\files\\";
                    var fileExtension = Path.GetExtension(uploadedFile.files.FileName);
                    var fileName = GetDate().ToString("yyyy-dd-M--HH-mm-ss") + Guid.NewGuid() + fileExtension;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    using (var fileStream = File.Create(Path.Combine(path, fileName)))
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
                return e.Message;
            }
        }

        public ActionResult<string> UploadImage(string entity, ControllerBase controller, FileUploadAPI uploadedFile)
        {
            try
            {
                if (uploadedFile.files.Length > 0)
                {
                    string path;
                    var fileExtension = Path.GetExtension(uploadedFile.files.FileName);
                    var fileName = GetDate().ToString("yyyy-dd-M--HH-mm-ss") + Guid.NewGuid() + fileExtension;
                    if (entity == "company")
                        path = _webHostEnvironment.ContentRootPath + "\\images\\company\\";
                    else if (entity == "logo")
                        path = _webHostEnvironment.ContentRootPath + "\\images\\logo\\";
                    else
                        path = _webHostEnvironment.ContentRootPath + "\\images\\signatures\\";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    using (var fileStream = File.Create(Path.Combine(path, fileName)))
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
                return e.Message;
            }
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(GetDate(), TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}