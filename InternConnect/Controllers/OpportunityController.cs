using System;
using System.Collections.Generic;
using InternConnect.Dto.Opportunity;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        public OpportunityController(IOpportunityService opportunity)
        {
            _opportunityService = opportunity;
        }


        //GET /accounts
        [HttpGet("{id}", Name = "GetOpportunity")]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetOpportunity(int id)
        {
            try
            {
                return Ok(_opportunityService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Opportunity doesn't exist");
            }
            
        }

        [HttpGet("company/{companyId}")]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetOpportunitiesForEachCompany(int companyId)
        {
            return Ok(_opportunityService.GetByCompanyId(companyId));
        }

        [HttpGet]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetAllOpportunity()
        {
            return Ok(_opportunityService.GetAllOpportunities());
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteOpportunity(int id)
        {
            _opportunityService.DeleteOpportunity(id);
            return Ok();
        }

        [HttpPut]
        public ActionResult<OpportunityDto.ReadOpportunity> UpdateOpportunity(OpportunityDto.UpdateOpportunity payload)
        {
            _opportunityService.UpdateOpportunity(payload);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<OpportunityDto.ReadOpportunity> AddOpportunity(OpportunityDto.AddOpportunity payload)
        {
            var opportunityData =_opportunityService.AddOpportunity(payload);
            return CreatedAtRoute(nameof(GetOpportunity), new { Id = opportunityData.Id }, opportunityData);
        }
        //Chairs


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

        //}
        //[Authorize]
        //[HttpDelete("{id}")]
        //public ActionResult DeleteMerch(int id)
        //{
        //    var photoFolderPath = _env.ContentRootPath + "/Photos/";
        //    var teamModelFromRepo = _repository.GetTeamById(id);
        //    if (teamModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.DeleteTeam(teamModelFromRepo);
        //    System.IO.File.Delete(photoFolderPath + teamModelFromRepo.TeamsImage);
        //    _repository.SaveChanges();
        //    return NoContent();
        //}
    }
}