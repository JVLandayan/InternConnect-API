using System;
using InternConnect.Dto.AdminResponse;

namespace InternConnect.Dto.Submission
{
    public class SubmissionDto
    {
        public class ReadSubmission
        {
            public int Id { get; set; }
            public int IsoCode { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public int StudentNumber { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonPosition { get; set; }
            public string AcceptanceLetterFileName { get; set; }
            public string CompanyProfileFileName { get; set; }
            public string JobDescription { get; set; }
            public string EndorsementFileName { get; set; }
            public int TrackId { get; set; }
            public int StudentId { get; set; }
            public int CompanyId { get; set; }
        }

        public class UpdateSubmission
        {
            public int Id { get; set; }
            public int IsoCode { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public int StudentNumber { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonPosition { get; set; }
            public string AcceptanceLetterFileName { get; set; }
            public string JobDescription { get; set; }
            public int TrackId { get; set; }
            public int StudentId { get; set; }
            public AdminResponseDto.AddResponse AdminResponse { get; set; }
        }

        public class AddSubmission
        {
            public int IsoCode { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public int StudentNumber { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonEmail { get; set; }
            public string ContactPersonPosition { get; set; }
            public string AcceptanceLetterFileName { get; set; }
            public string CompanyProfileFileName { get; set; }
            public string JobDescription { get; set; }
            public string EndorsementFileName { get; set; }
            public int TrackId { get; set; }
            public int CompanyId { get; set; }
            public int StudentId { get; set; }
        }
    }
}