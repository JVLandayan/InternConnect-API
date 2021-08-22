using InternConnect.Context.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.AdminResponse
{
    public class AdminResponseDto
    {
        public class AddResponse
        {
            public bool? AcceptedByCoordinator { get; set; }
            public bool? AcceptedByChair { get; set; }
            public bool? AcceptedByDean { get; set; }
            public bool? EmailSentByCoordinator { get; set; }
            public bool? CompanyAgrees { get; set; }
            public string Comments { get; set; }
        }

        public class UpdateCoordinatorResponse
        {
            public int Id { get; set; }
            public bool AcceptedByCoordinator { get; set; }
            public bool EmailSentByCoordinator { get; set; }
            public bool CompanyAgrees { get; set; }
            public string Comments { get; set; }
        }

        public class UpdateDeanResponse
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public bool AcceptedByDean { get; set; }
            public string Comments { get; set; }
        }
        public class UpdateChairResponse
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public bool AcceptedByChair { get; set; }
            public string Comments { get; set; }
        }

        public class ReadResponse
        {
            public int Id { get; set; }
            public bool AcceptedByCoordinator { get; set; }
            public bool AcceptedByChair { get; set; }
            public bool AcceptedByDean { get; set; }
            public bool EmailSentByCoordinator { get; set; }
            public bool CompanyAgrees { get; set; }
            public string Comments { get; set; }
            public Context.Models.Submission Submission { get; set; }

        }
    }
}
