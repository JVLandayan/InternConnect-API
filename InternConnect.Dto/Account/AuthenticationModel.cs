using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.Account
{
    public class AuthenticationModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}