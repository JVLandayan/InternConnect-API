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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService admin, InternConnectContext context)
        {
            _adminService = admin;
        }

        
        //GET /accounts
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            return Ok(_accountService.GetAll());
        }


        //Coordinators
        //Authorize AuthCoordinatorClaim
        [HttpPut("admin")]
        public ActionResult<AccountDto.ReadAccount> AddCoordinators(AccountDto.AddAccountCoordinator payload)
        {
            _accountService.AddCoordinator(payload);
            return Ok();
        }

        //Authorize Student Claim
        // POST accounts/student 
        [HttpPost("student")]
        public ActionResult<AccountDto.ReadAccount> AddCoordinators(AccountDto.AddAccountStudent payload)
        {
            _accountService.AddStudent(payload);
            return Ok();
        }

        //Chairs
        [HttpPost("chair")]
        public ActionResult<AccountDto.ReadAccount> AddChairs(AccountDto.AddAccountChair payload)
        {
            _accountService.AddChair(payload);
            return Ok();
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
