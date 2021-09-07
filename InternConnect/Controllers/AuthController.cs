using InternConnect.Dto.Account;
using InternConnect.Service.ThirdParty;
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
        public ActionResult Authenticate()
        {
            return Ok();
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
    }
}