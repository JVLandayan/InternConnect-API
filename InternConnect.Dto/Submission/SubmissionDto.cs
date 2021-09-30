using System;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.Company;
using InternConnect.Dto.Student;

namespace InternConnect.Dto.Submission
{
    public class SubmissionDto
    {
        public class ReadSubmission
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
            public StudentDto.ReadStudent Student { get; set; }
            public int CompanyId { get; set; }
            public CompanyDto.ReadCompany Company { get; set; }
            public AdminResponseDto.ReadResponse AdminResponse { get; set; }
        }

        public class UpdateSubmission
        {
            public int Id { get; set; }
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
            public string JobDescription { get; set; }
            public int TrackId { get; set; }
            public int StudentId { get; set; }
            public AdminResponseDto.AddResponse AdminResponse { get; set; }
        }

        public class AddSubmission
        {
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
            public int CompanyId { get; set; }
            public int StudentId { get; set; }
        }
    }
}