using System;
using System.Collections.Generic;
using System.IO;
using InternConnect.Dto.Submission;
using InternConnect.Service.Main;
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

        public SubmissionController(ISubmissionService submission, IWebHostEnvironment webHostEnvironment )
        {
            _submissionService = submission;
            _webHostEnvironment = webHostEnvironment;
        }


        //GET /accounts
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

        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDto.ReadSubmission>> GetAllSubmission()
        {
            return Ok(_submissionService.GetAllSubmissions());
        }

        [HttpPut]
        public ActionResult<SubmissionDto.ReadSubmission> UpdateSubmission(SubmissionDto.UpdateSubmission payload)
        {
            _submissionService.UpdateSubmission(payload);
            return NoContent();
        }

        [HttpPost("{sectionId}")]
        public ActionResult AddSubmission(SubmissionDto.AddSubmission payload, int sectionId)
        {
            var submissionData =_submissionService.AddSubmission(payload, sectionId);
            return CreatedAtRoute(nameof(GetSubmission), new { Id = submissionData.Id }, submissionData);
        }
    }
}