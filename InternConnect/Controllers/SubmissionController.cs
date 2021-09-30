using System;
using System.Collections.Generic;
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
        [HttpGet("{id}", Name = "GetSubmission")]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetSubmission(int id)
        {
            try
            {
                return Ok(_submissionService.GetSubmission(id));
            }
            catch (Exception e)
            {
                return BadRequest("Submission doesn't exist");
            }
        }

//        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetAllSubmission()
        {
            return Ok(_submissionService.GetAllSubmissions());
        }

        //[Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
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

        //[Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult AddSubmission(SubmissionDto.AddSubmission payload, int sectionId, int programId)
        {
            var submissionData = _submissionService.AddSubmission(payload, sectionId, programId);
            return CreatedAtRoute(nameof(GetSubmission), new {submissionData.Id}, submissionData);
        }
    }
}