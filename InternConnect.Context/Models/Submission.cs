using System;
using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int IsoCode { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string StudentTitle { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public int StudentNumber { get; set; }
        public string ContactPersonTitle { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPosition { get; set; }
        public string AcceptanceLetterFileName { get; set; }
        public string CompanyProfileFileName { get; set; }
        public string JobDescription { get; set; }
        public int TrackId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CompanyId { get; set; }
        public AdminResponse AdminResponse { get; set; }
        public List<Logs> Logs { get; set; }
    }
}