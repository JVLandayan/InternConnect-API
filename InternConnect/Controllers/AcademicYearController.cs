    using System;
    using System.Collections.Generic;
    using System.Net;
    using InternConnect.Dto.AcademicYear;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYear)
        {
            _academicYearService = academicYear;
        }

        //GET /admin

        //GET /admin/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AcademicYearDto.ReadAcademicYear>> GetAcademicYear(int id)
        {
            try
            {
                if (_academicYearService.GetAcademicYear() != null)
                {
                    return Ok(_academicYearService.GetAcademicYear());
                }

                    throw new Exception();

                


            }
            catch (Exception e)
            {
                return BadRequest("Academic Year isn't set");
            }


            
        }


        [HttpPut]
        public ActionResult<AcademicYearDto.ReadAcademicYear> UpdateAcademicYear(
            AcademicYearDto.UpdateAcademicYear payload)
        {
            try
            {
                _academicYearService.UpdateAcademicYear(payload);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

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