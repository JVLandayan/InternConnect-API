using System;
using System.Collections.Generic;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Dto.Account;
using InternConnect.Service.Main;
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
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            return Ok(_accountService.GetAll());
        }

        [HttpGet("{id}", Name = "GetAccount")]
        public ActionResult<IEnumerable<Account>> GetAccount(int id)
        {
            try
            {
                return Ok(_accountService.GetById(id));
            }
            catch (Exception e)
            {
                return NotFound(new { message = "Account doesn't exist" });
            }
        }



        [HttpPost("coordinators")]
        public ActionResult<AccountDto.ReadAccount> AddCoordinators(AccountDto.AddAccountCoordinator payload)
        {
            var accountData = _accountService.AddCoordinator(payload);
            if (accountData.Id  == 0)
            {
                return BadRequest("Email already exists");
            }
            return CreatedAtRoute(nameof(GetAccount), new { Id = accountData.Id }, accountData);
        }

        [HttpPost("student")]
        public ActionResult<AccountDto.ReadAccount> AddStudents(AccountDto.AddAccountStudent payload)
        {
           var accountData =  _accountService.AddStudent(payload);
           if (accountData.Id == 0)
           {
               return BadRequest("Email already exists");
           }
           return CreatedAtRoute(nameof(GetAccount), new { Id = accountData.Id }, accountData);
        }

        //Chairs
        [HttpPost("chair")]
        public ActionResult<AccountDto.ReadAccount> AddChairs(AccountDto.AddAccountChair payload)
        {
            var accountData = _accountService.AddChair(payload);
            if (accountData.Id == 0)
            {
                return BadRequest("Email already exists");
            }
            return CreatedAtRoute(nameof(GetAccount), new { Id = accountData.Id }, accountData);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            _accountService.Delete(id);
            return NoContent();
        }

        
    }
}