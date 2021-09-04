using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternConnect.Context.Models;
using InternConnect.Dto.AcademicYear;
using InternConnect.Service.Main;
using InternConnect.Service.ThirdParty;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadService _uploadService;

        public FileController(IReportService reportService, IWebHostEnvironment webHostEnvironment, IUploadService uploadService)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
            _uploadService = uploadService;
        }



        [HttpGet("excel")]
        public IActionResult GenerateExcel([FromQuery] int[] ids)
        {
            if (ids.Length == 0)
            {
                return NotFound("Submission not found");
            }

            return _reportService.GenerateExcel(ids, this);
        }

        [HttpPost("file")]
        public ActionResult<string> FileUpload([FromForm] FileUploadAPI uploadedFile )
        {
            return _uploadService.SubmissionFiles(this, uploadedFile);
        }

        [HttpPost("image/{entity}")]
        public ActionResult<string> ImageUpload([FromForm] FileUploadAPI uploadedFile, string entity)
        {
            return _uploadService.UploadImage(entity, this, uploadedFile);
        }






        //[Authorize]
        //[HttpPut("{id}")]

        //public ActionResult UpdateTeam(int id, TeamsUpdateDto teamsUpdateDto)
        //{
        //    var teamsModelFromRepo = _repository.GetTeamById(id);

        //    if (teamsModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(teamsUpdateDto, teamsModelFromRepo);
        //    _repository.UpdateTeam(teamsModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();

        //}
        //[Authorize]
        //[HttpPatch("{id}")]

        //public ActionResult PartialTeamsUpdate(int id, JsonPatchDocument<TeamsUpdateDto> patchDoc)
        //{
        //    var teamModelFromRepo = _repository.GetTeamById(id);
        //    if (teamModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    var teamToPatch = _mapper.Map<TeamsUpdateDto>(teamModelFromRepo);
        //    patchDoc.ApplyTo(teamToPatch, ModelState);
        //    if (!TryValidateModel(teamToPatch))
        //    {
        //        return ValidationProblem();
        //    }
        //    _mapper.Map(teamToPatch, teamModelFromRepo);
        //    _repository.UpdateTeam(teamModelFromRepo);
        //    _repository.SaveChanges();
        //    return NoContent();
    }
}