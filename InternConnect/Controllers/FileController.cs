using System.IO;
using InternConnect.Context.Models;
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
        [HttpGet("excel")]
        public IActionResult GenerateExcel([FromQuery] int[] ids)
        {
            if (ids.Length == 0) return NotFound("Submission not found");

            return _reportService.GenerateExcel(ids, this);
        }

        //[Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
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

        [HttpGet("{adminId}/previewPdf")]
        public ActionResult PreviewPdf(int adminId)
        {
            return _pdfService.PreviewPdf(this, adminId);
        }
    }
}