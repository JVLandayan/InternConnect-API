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
        public ActionResult<AccountDto.ReadAccount> ResetPassword(string email, string password, string resetKey)
        {
            var accountData = _authService.ResetPassword(email, password, resetKey);
            if (accountData == null) return BadRequest("Account not found");
            _authService.ResetPassword(email, password, resetKey);
            return Ok("Password resetted");
        }
    }
}