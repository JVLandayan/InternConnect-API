using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Service.ThirdParty
{
    public interface IReportService
    {
        public IActionResult GenerateExcel(int[] idList, ControllerBase controller);
    }

    public class ReportService : IReportService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProgramRepository _programRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly ISectionRepository _sectionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public ReportService(ISubmissionRepository submissionRepository, IStudentRepository studentRepository,
            IAccountRepository accountRepository, ISectionRepository sectionRepository,
            ICompanyRepository companyRepository,
            IProgramRepository programRepository, IWebHostEnvironment env)
        {
            _submissionRepository = submissionRepository;
            _studentRepository = studentRepository;
            _accountRepository = accountRepository;
            _companyRepository = companyRepository;
            _sectionRepository = sectionRepository;
            _programRepository = programRepository;
            _environment = env;
        }


        public IActionResult GenerateExcel(int[] idList, ControllerBase controller)
        {
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"{DateTime.Now}.xlsx";

            using (var workBook = new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Submissions");

                #region Headers
                worksheet.Cell(1, 1).Value = "Email Address";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Value = "ISO Code";
                worksheet.Cell(1, 3).Value = "Student Title";
                worksheet.Cell(1, 4).Value = "Lastname of Requesting Students";
                worksheet.Cell(1, 5).Value = "Firstname of Requesting Student";
                worksheet.Cell(1, 6).Value = "Middle Initial of Requesting Student";
                worksheet.Cell(1, 7).Value = "Student Number";
                worksheet.Cell(1, 8).Value = "Section";
                worksheet.Cell(1, 9).Value = "Degree Program";
                worksheet.Cell(1, 10).Value = "No. of Internship Hrs.";
                worksheet.Cell(1, 11).Value = "Contact Person Title";
                worksheet.Cell(1, 12).Value = "Contact Person First Name";
                worksheet.Cell(1, 13).Value = "Contact Person Last Name";
                worksheet.Cell(1, 14).Value = "Email of Contact Person";
                worksheet.Cell(1, 15).Value = "Official Designation";
                worksheet.Cell(1, 16).Value = "Name of Company/Institution";
                worksheet.Cell(1, 17).Value = "Company Address Line 1";
                worksheet.Cell(1, 18).Value = "Company Address Line 2";
                worksheet.Cell(1, 19).Value = "Company Address Line 3";
                worksheet.Cell(1, 20).Value = "City";
                worksheet.Cell(1, 21).Value = "Submission Date";
                worksheet.Cell(1, 22).Value = "STATUS";
                worksheet.Cell(1, 23).Value = "Acceptance Letter";
                worksheet.Cell(1, 24).Value = "Company Profile";
                for (int i = 1; i < 25; i++)
                {
                    worksheet.Cell(1, i).Style.Font.Bold = true;
                }


                #endregion

                #region Repositories

                var submissionList = _submissionRepository.GetAllRelatedData().ToList();
                var sectionList = _sectionRepository.GetAll().ToList();
                var programList = _programRepository.GetAll().ToList();
                var companyList = _companyRepository.GetAll().ToList();
                var accountList = _accountRepository.GetAll().ToList();

                #endregion

                var mappedSubmissionList = new List<Submission>();
                foreach (var id in idList) mappedSubmissionList.Add(submissionList.First(s => s.Id == id));

                #region Body

                var index = 1;
                foreach (var submission in mappedSubmissionList)
                {
                    worksheet.Cell(1 + index, 1).Value =
                        accountList.Find(s => s.Id == submission.Student.AccountId)
                            .Email; //_accountRepository.Get(_studentRepository.Get(_submissionRepository.Get(submission.Id).StudentId).AccountId).Email;
                    worksheet.Cell(1 + index, 2).Value = submission.IsoCode;
                    worksheet.Cell(1 + index, 3).Value = submission.StudentTitle;
                    worksheet.Cell(1 + index, 4).Value = submission.LastName;
                    worksheet.Cell(1 + index, 5).Value = submission.FirstName;
                    worksheet.Cell(1 + index, 6).Value = submission.MiddleInitial;
                    worksheet.Cell(1 + index, 7).Value = submission.StudentNumber;
                    worksheet.Cell(1 + index, 8).Value =
                        sectionList.Find(s => s.Id == submission.Student.SectionId).Name;
                    worksheet.Cell(1 + index, 9).Value =
                        programList.Find(p => p.Id == submission.Student.ProgramId).Name;
                    worksheet.Cell(1 + index, 10).Value =
                        programList.Find(p => p.Id == submission.Student.ProgramId).NumberOfHours;
                    worksheet.Cell(1 + index, 11).Value = submission.ContactPersonTitle;
                    worksheet.Cell(1 + index, 12).Value = submission.ContactPersonFirstName;
                    worksheet.Cell(1 + index, 13).Value = submission.ContactPersonLastName;
                    worksheet.Cell(1 + index, 14).Value = submission.ContactPersonEmail;
                    worksheet.Cell(1 + index, 15).Value = submission.ContactPersonPosition;
                    worksheet.Cell(1 + index, 16).Value = companyList.Find(c => c.Id == submission.CompanyId).Name;
                    worksheet.Cell(1 + index, 17).Value =
                        companyList.Find(c => c.Id == submission.CompanyId).AddressOne;
                    worksheet.Cell(1 + index, 18).Value =
                        companyList.Find(c => c.Id == submission.CompanyId).AddressTwo;
                    worksheet.Cell(1 + index, 19).Value =
                        companyList.Find(c => c.Id == submission.CompanyId).AddressThree;
                    worksheet.Cell(1 + index, 20).Value = companyList.Find(c => c.Id == submission.CompanyId).City;
                    worksheet.Cell(1 + index, 21).Value = submission.SubmissionDate;
                    // worksheet.Cell(1 + index, 19).Value = 

                    #region Status Logic Value

                    if (submission.AdminResponse.AcceptedByCoordinator == null &&
                        submission.AdminResponse.AcceptedByChair == null)
                        worksheet.Cell(1 + index, 22).Value = "Request Submitted";
                    else if (submission.AdminResponse.AcceptedByCoordinator == true &&
                             submission.AdminResponse.AcceptedByChair == null)
                        worksheet.Cell(1 + index, 22).Value = "Accepted By Coordinator";
                    else if (submission.AdminResponse.AcceptedByChair == true &&
                             submission.AdminResponse.AcceptedByDean == null)
                        worksheet.Cell(1 + index, 22).Value = "Accepted By Chair";
                    else if (submission.AdminResponse.AcceptedByDean == true &&
                             submission.AdminResponse.EmailSentByCoordinator == null)
                        worksheet.Cell(1 + index, 22).Value = "Accepted By Dean";
                    else if (submission.AdminResponse.EmailSentByCoordinator == true &&
                             submission.AdminResponse.CompanyAgrees == null)
                        worksheet.Cell(1 + index, 22).Value = "Email sent to company rep";
                    else
                        worksheet.Cell(1 + index, 22).Value = "Student Accepted";

                    #endregion

                    worksheet.Cell(1 + index, 23).Value = $"http://localhost:5000/files/{submission.AcceptanceLetterFileName}";
                    worksheet.Cell(1 + index, 24).Value = $"http://localhost:5000/files/{submission.CompanyProfileFileName}";
                    index++;
                }
                worksheet.Rows().AdjustToContents();
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                #endregion


                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return controller.File(content, contentType, fileName);
                }
            }
        }
    }
}