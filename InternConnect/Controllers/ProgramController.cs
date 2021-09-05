using System;
using System.Collections.Generic;
using InternConnect.Context;
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
            return CreatedAtRoute(nameof(GetProgram), new { Id = programData.Id }, programData);
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


        //[HttpPut("admin/{id}")]
        //public ActionResult<AccountDto.ReadAccount> UpdateSignature(AdminDto.UpdateAdmin payload, int id)
        //{
        //    _adminService.UpdateAdmin(payload, id);
        //    return NoContent();
        //}


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