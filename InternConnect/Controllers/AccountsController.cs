using System;
using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Dto.Account;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService account)
        {
            _accountService = account;
        }

        //GET /accounts
        [Authorize(Roles = "Dean")]
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            return Ok(_accountService.GetAll());
        }
        [Authorize(Roles = "Dean")]
        [HttpGet("{id}", Name = "GetAccount")]
        public ActionResult<IEnumerable<Account>> GetAccount(int id)
        {
            try
            {
                return Ok(_accountService.GetById(id));
            }
            catch (Exception e)
            {
                return NotFound(new {message = "Account doesn't exist"});
            }
        }

        [Authorize(Roles = "Chair")]
        [HttpPost("coordinators")]
        public ActionResult<AccountDto.ReadAccount> AddCoordinators(AccountDto.AddAccountCoordinator payload)
        {
            var accountData = _accountService.AddCoordinator(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
        }
        [Authorize (Roles = "Coordinator,Chair")]
        [HttpPost("student")]
        public ActionResult<AccountDto.ReadAccount> AddStudents(AccountDto.AddAccountStudent payload)
        {
            var accountData = _accountService.AddStudent(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
        }

        [HttpPost("techcoordinator")]
        public ActionResult<AccountDto.ReadAccount> AddStudents(AccountDto.AddAccountTechCoordinator payload)
        {
            var accountData = _accountService.AddTechCoordinator(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new { accountData.Id }, accountData);
        }

        //Chairs
        [Authorize(Roles = "Dean")]
        [HttpPost("chair")]
        public ActionResult<AccountDto.ReadAccount> AddChair(AccountDto.AddAccountChair payload)
        {
            var accountData = _accountService.AddChair(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
        }

        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            _accountService.Delete(id);
            return NoContent();
        }
    }
}