using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public int StudentNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string AcceptanceLetterFileName { get; set; }
        public string CompanyProfileFileName { get; set; }
        public bool AcceptedByCoordinator { get; set; }
        public bool AcceptedByChair { get; set; }
        public bool AcceptedByDean { get; set; }
        public bool EmailSentByCoordinator { get; set; }
        public bool CompanyAgrees { get; set; }
        public string EndorsementFileName { get; set; }

        public int Track { get; set; }

        public Student Student { get; set; }

        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }



    }
}
