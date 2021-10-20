using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Dto;
using InternConnect.Dto.Account;
using InternConnect.Service.Main;
using InternConnect.Service.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public AccountsController(IAccountService account, IAuthService auth)
        {
            _accountService = account;
            _authService = auth;
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
            if (_accountService.GetById(id) != null) return Ok(_accountService.GetById(id));
            return NotFound(new {message = "Account doesn't exist"});
        }

        [Authorize(Roles = "Chair")]
        [HttpPost("coordinators")]
        public ActionResult<AccountDto.ReadAccount> AddCoordinators(AccountDto.AddAccountCoordinator payload)
        {
            var accountData = _accountService.AddCoordinator(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
        }

        [Authorize(Roles = "Coordinator, Chair")]
        [HttpPost("student")]
        public ActionResult<AccountDto.ReadAccount> AddStudent(AccountDto.AddAccountStudent payload)
        {
            var accountData = _accountService.AddStudent(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
        }

        //[Authorize(Roles = "Coordinator, Chair")]
        [HttpPost("students")]
        public ActionResult<AccountDto.AddAccountStudent> AddStudents(List<AccountDto.AddAccountStudent> payload)
        {
            var accountData = _accountService.AddStudents(payload);
            //if (accountData.Id == 0) return BadRequest("Email already exists");
            if (accountData.Count == 0) return Ok();

            return Ok(accountData);
        }

        [Authorize(Roles = "Dean")]
        [HttpPost("techcoordinator")]
        public ActionResult<AccountDto.ReadAccount> AddTechCoordinator(AccountDto.AddAccountTechCoordinator payload)
        {
            var accountData = _accountService.AddTechCoordinator(payload);
            if (accountData.Id == 0) return BadRequest("Email already exists");
            return CreatedAtRoute(nameof(GetAccount), new {accountData.Id}, accountData);
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

        //[Authorize(Roles = "Dean")]
        [HttpPost("DeleteAll")]
        public ActionResult<string> DeleteAllAccounts(AuthenticationModel payload, string startDate, string endDate)
        {
            if (_authService.Authenticate(payload) == null) return BadRequest("Wrong Password");
            _accountService.DeleteAll(startDate, endDate);
            return NoContent();
        }

        [Authorize(Roles = "Dean")]
        [HttpPost("changedean")]
        public ActionResult<string> ChangeDean(ChangeDeanModel payload)
        {
            var accountData = _accountService.ChangeDean(payload);

            if (accountData == null) return Ok("Email sent to the new Dean");

            return BadRequest("Invalid password, please try again");
        }
    }
}