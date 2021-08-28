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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProgramDto.ReadProgram>> GetProgram(int id)
        {
            return Ok(_programService.GetById(id));
        }

        [HttpPost]
        public ActionResult<ProgramDto.ReadProgram> AddProgram(ProgramDto.AddProgram payload)
        {
            _programService.AddProgram(payload);
            return Ok();
        }

        [HttpPut("ISO")]
        public ActionResult<ProgramDto.ReadProgram> UpdateIsoCode(ProgramDto.UpdateIsoCode payload)
        {
            _programService.UpdateIsoCode(payload);
            return Ok();
        }

        [HttpPut("program")]
        public ActionResult<ProgramDto.ReadProgram> UpdateProgram(ProgramDto.UpdateProgram payload)
        {
            _programService.UpdateProgram(payload);
            return Ok();
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