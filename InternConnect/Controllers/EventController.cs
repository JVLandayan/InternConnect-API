using System.Collections.Generic;
using InternConnect.Dto.Event;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Chair")]
        [HttpGet("all/{adminId}")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetAllEvents(int adminId)
        {
            return Ok(_eventsService.GetAll(adminId));
        }

        [Authorize]
        [HttpGet("student/{programId}")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetAllEventsByProgramId(int programId)
        {
            return Ok(_eventsService.GetAllByProgramId(programId));
        }

        //GET /admin/id
        [Authorize(Roles = "Chair, Coordinator, Student")]
        [HttpGet("{id}", Name = "GetEvent")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetEvent(int id)
        {
            if (_eventsService.GetbyId(id) != null) return Ok(_eventsService.GetbyId(id));
            return BadRequest("Event doesn't exist");
        }

        [Authorize(Roles = "Chair")]
        [HttpPost("{adminId}")]
        public ActionResult<EventDto.ReadEvent> AddEvent(EventDto.AddEvent payload, int adminId)
        {
            var eventData = _eventsService.AddEvent(payload, adminId);
            return CreatedAtRoute(nameof(GetEvent), new { eventData.Id }, eventData);
        }

        [Authorize(Roles = "Chair")]
        [HttpPut("admin")]
        public ActionResult<EventDto.ReadEvent> UpdateEvent(EventDto.UpdateEvent payload)
        {
            _eventsService.UpdateEvent(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteEvent(int id)
        {
            _eventsService.DeleteEvent(id);
            return NoContent();
        }
    }
}