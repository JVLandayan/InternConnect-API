using System;
using System.Collections.Generic;
using InternConnect.Dto.Track;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService track)
        {
            _trackService = track;
        }


        //GET /admin
        [HttpGet]
        public ActionResult<IEnumerable<TrackDto.ReadTrack>> GetAllTrack()
        {
            return Ok(_trackService.GetAllTracks());
        }

        //GET /admin/id
        [HttpGet("{id}", Name = "GetTrack")]
        public ActionResult<IEnumerable<TrackDto.ReadTrack>> GetTrack(int id)
        {
            try
            {
                return Ok(_trackService.GetTrack(id));
            }
            catch (Exception e)
            {
                return BadRequest("Track doesn't exist");
            }

        }


        [HttpPut]
        public ActionResult<TrackDto.ReadTrack> UpdateTrack(TrackDto.UpdateTrack payload)
        {
            _trackService.UpdateTrack(payload);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<TrackDto.ReadTrack> AddTrack(TrackDto.AddTrack payload)
        {
            var trackData = _trackService.AddTrack(payload);
            return CreatedAtRoute(nameof(GetTrack), new { Id = trackData.Id }, trackData);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTrack(int id)
        {
            try
            {
                _trackService.DeleteTrack(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("Track doesn't exist");
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