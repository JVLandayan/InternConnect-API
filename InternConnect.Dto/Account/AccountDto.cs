using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;

namespace InternConnect.Dto.Account
{
    public class AccountDto
    {
        public class ReadAccount
        {
            public int Id { get; set; }
            public string Email { get; set; }

        }
        public class AddAccount
        {
            public string Email { get; set; }
        }
        public class UpdateAccount
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ResetKey { get; set; }

        }
    }
}
