    using System;
using System.Collections.Generic;
using InternConnect.Dto;
using InternConnect.Dto.Submission;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubmissionController(ISubmissionService submission, IWebHostEnvironment webHostEnvironment)
        {
            _submissionService = submission;
            _webHostEnvironment = webHostEnvironment;
        }


        //GET /accounts
        [Authorize]
        [HttpGet("{studentId}", Name = "GetSubmissionByStudentId")]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetSubmissionByStudentId(int studentId)
        {
            try
            {
                return Ok(_submissionService.GetSubmission(studentId));
            }
            catch (Exception e)
            {
                return Ok(null);
            }
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetAllSubmission()
        {
            return Ok(_submissionService.GetAllSubmissions());
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("program/{programId}")]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetSubmissionByProgram(int programId)
        {
            return Ok(_submissionService.GetSubmissionByProgram(programId));
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("section/{sectionId}")]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetSubmissionBySection(int sectionId)
        {
            return Ok(_submissionService.GetSubmissionBySection(sectionId));
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("bystep/{stepNumber}")]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetSubmissionByStep(int stepNumber)
        {
            return Ok(_submissionService.GetSubmissionsByStep(stepNumber));
        }

        [Authorize(Roles = "Student")]
        [HttpPut]
        public ActionResult<SubmissionDto.ReadSubmission> UpdateSubmission(SubmissionDto.UpdateSubmission payload)
        {
            _submissionService.UpdateSubmission(payload);
            return NoContent();
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult AddSubmission(SubmissionDto.AddSubmission payload, int sectionId, int programId)
        {
            var submissionData = _submissionService.AddSubmission(payload, sectionId, programId);
            return Ok();
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet("dashboard/{type}/{id}")]
        public ActionResult<IEnumerable<CompanyAndNumberOfStudentModel>>
            GetSubmissionByHighestNumberOfCompanyOccurence(string type, int id)
        {
            return Ok(_submissionService.GetSubmissionByNumberOfCompanyOccurence(type, id));
        }
    }
}