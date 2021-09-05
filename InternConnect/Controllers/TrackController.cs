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
            return CreatedAtRoute(nameof(GetTrack), new {trackData.Id}, trackData);
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
    }
}