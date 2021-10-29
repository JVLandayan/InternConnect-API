using System;
using System.ComponentModel.DataAnnotations;
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

        public class SubmissionReports
        {
            public int Id { get; set; }
            public int IsoCode { get; set; }
            public DateTime SubmissionDate { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string CompanyName { get; set; }
            public string ContactPersonEmail { get; set; }
            public string JobDescription { get; set; }
            public string SubmissionStatus { get; set; }
            public int SectionId { get; set; }
            public int ProgramId { get; set; }
            public string Comments { get; set; }
        }

        public class SubmissionStatus
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
            public string StudentEmail { get; set; }
            public string CompanyName { get; set; }
            public string CompanyAddressOne { get; set; }
            public string CompanyAddressTwo { get; set; }
            public string CompanyAddressThree { get; set; }
            public int AdminResponseId { get; set; }
        }


        public class ReadStudentData
        {
            public int CompanyId { get; set; }
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
            public string CompanyProfileFileName { get; set; }  
            public string ContactPersonPosition { get; set; }
            public string AcceptanceLetterFileName { get; set; }
            public string JobDescription { get; set; }
            public int CompanyId { get; set; }
            public int TrackId { get; set; }
            public int StudentId { get; set; }
        }

        public class AddSubmission
        {
            [Required] public string StudentTitle { get; set; }
            [Required] public string LastName { get; set; }
            [Required] public string FirstName { get; set; }
            [Required] public string MiddleInitial { get; set; }
            [Required] public int StudentNumber { get; set; }
            [Required] public string ContactPersonTitle { get; set; }
            [Required] public string ContactPersonFirstName { get; set; }
            [Required] public string ContactPersonLastName { get; set; }
            [Required] public string ContactPersonEmail { get; set; }
            [Required] public string ContactPersonPosition { get; set; }
            [Required] public string AcceptanceLetterFileName { get; set; }
            [Required] public string CompanyProfileFileName { get; set; }
            [Required] public string JobDescription { get; set; }
            [Required] public int TrackId { get; set; }
            [Required] public int CompanyId { get; set; }
            [Required] public int StudentId { get; set; }
        }
    }
}