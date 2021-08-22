using InternConnect.Service.Main.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data;
using InternConnect.Dto.Account;
using InternConnect.Dto.Admin;
using InternConnect.Dto.Event;
using InternConnect.Dto.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<EventDto.ReadEvent>> GetEvent(int id)
        {
            return Ok(_eventsService.GetbyId(id));
        }

        [HttpPost]
        public ActionResult<EventDto.ReadEvent> AddEvent(EventDto.AddEvent payload)
        {
            _eventsService.AddEvent(payload);
            return Ok();
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
