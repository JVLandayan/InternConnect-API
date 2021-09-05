using System;
using System.Collections.Generic;
using InternConnect.Dto.Event;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventsService;

        public EventController(IEventService events)
        {
            _eventsService = events;
        }


        //GET /admin
        [HttpGet("all/{adminId}")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetAllEvents(int adminId)
        {
            return Ok(_eventsService.GetAll(adminId));
        }

        //GET /admin/id
        [HttpGet("{id}", Name = "GetEvent")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetEvent(int id)
        {
            try
            {
                return Ok(_eventsService.GetbyId(id));
            }
            catch (Exception e)
            {
                return BadRequest("Event doesn't exist");
            }

        }

        [HttpPost]
        public ActionResult<EventDto.ReadEvent> AddEvent(EventDto.AddEvent payload)
        {
            var eventData = _eventsService.AddEvent(payload);
            return CreatedAtRoute(nameof(GetEvent), new { Id = eventData.Id }, eventData);
        }

        [HttpPut("admin")]
        public ActionResult<EventDto.ReadEvent> UpdateEvent(EventDto.UpdateEvent payload)
        {
            _eventsService.UpdateEvent(payload);
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