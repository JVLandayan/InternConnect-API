using System;
using System.Collections.Generic;
using InternConnect.Dto.Section;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService section)
        {
            _sectionService = section;
        }


        //GET /admin
        [HttpGet]
        public ActionResult<IEnumerable<SectionDto.ReadSection>> GetAllSections()
        {
            return Ok(_sectionService.GetAll());
        }

        //GET /admin/id
        [HttpGet("{id}", Name = "GetSection")]
        public ActionResult<IEnumerable<SectionDto.ReadSection>> GetSection(int id)
        {
            try
            {
                return Ok(_sectionService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Section doesn't exist");
            }
            
        }


        [HttpPut]
        public ActionResult<SectionDto.ReadSection> UpdateSection(SectionDto.UpdateSection payload)
        {
            _sectionService.UpdateSection(payload);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<SectionDto.ReadSection> AddSection(SectionDto.AddSection payload)
        {
            var sectionData = _sectionService.AddSection(payload);
            return CreatedAtRoute(nameof(GetSection), new { Id = sectionData.Id }, sectionData);
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