using System;
using System.Collections.Generic;
using InternConnect.Dto.Program;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<ProgramDto.ReadProgram>> GetAllPrograms()
        {
            return Ok(_programService.GetAll());
        }

        //GET /admin/id
        [Authorize]
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

        [Authorize(Roles = "Dean")]
        [HttpPost]
        public ActionResult<ProgramDto.ReadProgram> AddProgram(ProgramDto.AddProgram payload)
        {
            var programData = _programService.AddProgram(payload);
            return CreatedAtRoute(nameof(GetProgram), new {programData.Id}, programData);
        }


        [Authorize(Roles = "Dean")]
        [HttpPut("program")]
        public ActionResult<ProgramDto.ReadProgram> UpdateProgram(ProgramDto.UpdateProgram payload)
        {
            _programService.UpdateProgram(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteProgram(int id)
        {
            _programService.DeleteProgram(id);
            return NoContent();
        }
    }
}