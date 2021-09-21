using InternConnect.Dto.Account;
using InternConnect.Service.ThirdParty;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public ActionResult Authenticate(AuthenticationModel payload)
        {
            var sessionData =  _authService.Authenticate(payload);

            if (sessionData != null)
            {
                return Ok(sessionData);
            }

            return BadRequest("Wrong email or password");

        }
        
        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(string email)
        {
            var accountData = _authService.ForgotPassword(email);
            if (accountData == null) return BadRequest("Account not found");
            return Ok("Sent a password reset link to the email");
        }

        //Chairs
        [HttpPost("resetpassword")]
        public ActionResult<AccountDto.ReadAccount> ResetPassword(AccountDto.UpdateAccount payload)
        {
            var accountData = _authService.ResetPassword(payload);
            if (accountData == null) return BadRequest("Account not found");
            _authService.ResetPassword(payload);
            return Ok("Password Resetted");
        }

        [HttpPost("changedean")]
        public ActionResult<AccountDto.ReadAccount> ChangeDean(AccountDto.UpdateAccount payload, string oldEmail)
        {
            var accountData = _authService.ChangeDean(payload, oldEmail);
            if (accountData == null) return BadRequest("Account not found");
            _authService.ResetPassword(payload);
            return Ok("Password Resetted");
        }
    }
}