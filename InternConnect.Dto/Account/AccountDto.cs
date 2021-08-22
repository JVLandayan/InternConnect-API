using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using InternConnect.Dto.Admin;
using InternConnect.Dto.Student;

namespace InternConnect.Dto.Account
{
    public class AccountDto
    {
        public class ReadAccount
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public StudentDto.ReadStudent Student { get; set; }
            public AdminDto.ReadAdmin Admin { get; set; }

        }
        public class AddAccountCoordinator
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public int SectionId { get; set; }
            [Required]
            public int ProgramId { get; set; }
        }

        public class AddAccountChair
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public int ProgramId { get; set; }
        }

        public class AddAccountStudent
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public int SectionId { get; set; }
            [Required]
            public int ProgramId { get; set; }
            public string AdminEmail { get; set; }
        }

        public class MapAdmin
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ResetKey { get; set; }
        }


        public class UpdateAccount
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ResetKey { get; set; }

        }
    }
}
