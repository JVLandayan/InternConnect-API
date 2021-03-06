using System.ComponentModel.DataAnnotations.Schema;

namespace InternConnect.Context.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetKey { get; set; }

        [NotMapped] public string Token { get; set; }

        public Student Student { get; set; }
        public Admin Admin { get; set; }
    }
}