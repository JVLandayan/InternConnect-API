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
            return CreatedAtRoute(nameof(GetEvent), new {eventData.Id}, eventData);
        }

        [HttpPut("admin")]
        public ActionResult<EventDto.ReadEvent> UpdateEvent(EventDto.UpdateEvent payload)
        {
            _eventsService.UpdateEvent(payload);
            return NoContent();
        }
    }
}