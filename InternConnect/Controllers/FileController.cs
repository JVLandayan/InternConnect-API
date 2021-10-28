using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Dto;
using InternConnect.Service.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IPdfService _pdfService;
        private readonly IReportService _reportService;
        private readonly IUploadService _uploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IReportService reportService, IWebHostEnvironment webHostEnvironment,
            IUploadService uploadService, IPdfService pdfService)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
            _uploadService = uploadService;
            _pdfService = pdfService;
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpPost("excel")]
        public IActionResult GenerateExcel(List<ReportsId> idList)
        {
            if (idList.Count == 0) return NotFound("Submission not found");

            return _reportService.GenerateExcel(idList, this);
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("pdf/{submissionId}")]
        public IActionResult GeneratePdf(int submissionId)
        {
            return _pdfService.GeneratePdf(this, submissionId);
        }

        //[Authorize(Roles = "Student")]
        [HttpPost("file")]
        public ActionResult<string> FileUpload([FromForm] FileUploadAPI uploadedFile)
        {
            return _uploadService.SubmissionFiles(this, uploadedFile);
        }

        [Authorize]
        [HttpPost("image/{entity}")]
        public ActionResult<string> ImageUpload([FromForm] FileUploadAPI uploadedFile, string entity)
        {
            return _uploadService.UploadImage(entity, this, uploadedFile);
        }

        //[Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("{adminId}/previewPdf")]
        public IActionResult PreviewPdf(int adminId)
        {
            return _pdfService.PreviewPdf(this, adminId);
        }

        // z    
        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("previewPdf/submission/{submissionId}")]
        public IActionResult PreviewSubmissionPdf(int submissionId)
        {
            return _pdfService.GeneratePreviewPdf(this, submissionId);
        }
    }
}