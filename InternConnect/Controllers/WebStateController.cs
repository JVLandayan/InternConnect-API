using System;
using System.Collections.Generic;
using InternConnect.Dto.WebState;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebStateController : ControllerBase
    {
        private readonly IWebStateService _webStateService;

        public WebStateController(IWebStateService webState)
        {
            _webStateService = webState;
        }

        //GET /admin

        //GET /admin/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<WebStateDto.ReadWebState>> GetWebState(int id)
        {
            try
            {
                return Ok(_webStateService.GetWebState(id));
            }
            catch (Exception e)
            {
                return BadRequest("State doesn't exist");
            }

        }


        [HttpPut]
        public ActionResult<WebStateDto.ReadWebState> UpdateWebState(WebStateDto.UpdateWebState payload)
        {
            _webStateService.UpdateWebState(payload);
            return NoContent();
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