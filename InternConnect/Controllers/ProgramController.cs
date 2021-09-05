using System;
using System.Collections.Generic;
using InternConnect.Dto.Program;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService program)
        {
            _programService = program;
        }


        //GET /admin
        [HttpGet]
        public ActionResult<IEnumerable<ProgramDto.ReadProgram>> GetAllPrograms()
        {
            return Ok(_programService.GetAll());
        }

        //GET /admin/id
        [HttpGet("{id}", Name = "GetProgram")]
        public ActionResult<IEnumerable<ProgramDto.ReadProgram>> GetProgram(int id)
        {
            try
            {
                return Ok(_programService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Program doesn't exist");
            }
        }

        [HttpPost]
        public ActionResult<ProgramDto.ReadProgram> AddProgram(ProgramDto.AddProgram payload)
        {
            var programData = _programService.AddProgram(payload);
            return CreatedAtRoute(nameof(GetProgram), new {programData.Id}, programData);
        }

        [HttpPut("ISO")]
        public ActionResult<ProgramDto.ReadProgram> UpdateIsoCode(ProgramDto.UpdateIsoCode payload)
        {
            _programService.UpdateIsoCode(payload);
            return NoContent();
        }

        [HttpPut("program")]
        public ActionResult<ProgramDto.ReadProgram> UpdateProgram(ProgramDto.UpdateProgram payload)
        {
            _programService.UpdateProgram(payload);
            return NoContent();
        }

        [HttpPut("hours")]
        public ActionResult<ProgramDto.ReadProgram> UpdateHours(ProgramDto.UpdateNumberOfHours payload)
        {
            _programService.UpdateNumberOfHours(payload);
            return NoContent();
        }
    }
}