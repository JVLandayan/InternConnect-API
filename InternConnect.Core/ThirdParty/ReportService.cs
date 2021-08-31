using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Service.ThirdParty
{
    public interface IReportService
    {
        public IActionResult GenerateExcel(int[] idList, ControllerBase controller);
    }
    public class ReportService : IReportService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IProgramRepository _programRepository;

        public ReportService(ISubmissionRepository submissionRepository, IStudentRepository studentRepository, 
            IAccountRepository accountRepository, ISectionRepository sectionRepository, ICompanyRepository companyRepository,
            IProgramRepository programRepository)
        {
            _submissionRepository = submissionRepository;
            _studentRepository = studentRepository;
            _accountRepository = accountRepository;
            _companyRepository = companyRepository;
            _sectionRepository = sectionRepository;
            _programRepository = programRepository;
        }

        //public DataTable TableHeader()
        //{
        //    DataTable table = new DataTable();

        //    //Personal Info
        //    table.Columns.Add("Email Address", typeof(string));
        //    table.Columns.Add("ISO Code", typeof(int));
        //    table.Columns.Add("Lastname of Requesting Students", typeof(string));
        //    table.Columns.Add("Firstname of Requesting Student", typeof(string));
        //    table.Columns.Add("Middle Initial of Requesting Student", typeof(string));
        //    table.Columns.Add("Student Number", typeof(int));
        //    table.Columns.Add("Section", typeof(string));
        //    table.Columns.Add("Degree Program", typeof(string));
        //    table.Columns.Add("No. of Internship Hrs.", typeof(int));

        //    //Contact
        //    table.Columns.Add("Name of Contact Person", typeof(string));
        //    table.Columns.Add("Official Designation", typeof(string));

        //    //Company
        //    table.Columns.Add("Name of Company/Institution", typeof(string));
        //    table.Columns.Add("Company Address Line 1", typeof(string));
        //    table.Columns.Add("Company Address Line 2", typeof(string));
        //    table.Columns.Add("Company Address Line 3", typeof(string));
        //    table.Columns.Add("City", typeof(string));

        //    //Indicators
        //    table.Columns.Add("Submission Date", typeof(string));
        //    table.Columns.Add("STATUS", typeof(string));
        //    //Files
        //    table.Columns.Add("Acceptance Letter", typeof(string));
        //    table.Columns.Add("Company Profile", typeof(string));

        //    return table;
        //}


        public IActionResult GenerateExcel(int[] idList, ControllerBase controller)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"{DateTime.Now}.xlsx";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Submissions");

                #region Headers
                worksheet.Cell(1, 1).Value = "Email Address";
                worksheet.Cell(1, 2).Value = "ISO Code";
                worksheet.Cell(1, 3).Value = "Lastname of Requesting Students";
                worksheet.Cell(1, 4).Value = "Firstname of Requesting Student";
                worksheet.Cell(1, 5).Value = "Middle Initial of Requesting Student";
                worksheet.Cell(1, 6).Value = "Student Number";
                worksheet.Cell(1, 7).Value = "Section";
                worksheet.Cell(1, 8).Value = "Degree Program";
                worksheet.Cell(1, 9).Value = "No. of Internship Hrs.";
                worksheet.Cell(1, 10).Value = "Name of Contact Person";
                worksheet.Cell(1, 11).Value = "Email of Contact Person";
                worksheet.Cell(1, 12).Value = "Official Designation";
                worksheet.Cell(1, 13).Value = "Name of Company/Institution";
                worksheet.Cell(1, 14).Value = "Company Address Line 1";
                worksheet.Cell(1, 15).Value = "Company Address Line 2";
                worksheet.Cell(1, 16).Value = "Company Address Line 3";
                worksheet.Cell(1, 17).Value = "City";
                worksheet.Cell(1, 18).Value = "Submission Date";
                worksheet.Cell(1, 19).Value = "STATUS";
                worksheet.Cell(1, 20).Value = "Acceptance Letter";
                worksheet.Cell(1, 21).Value = "Company Profile";


                #endregion

                #region Repositories
                var submissionList = _submissionRepository.GetAllRelatedData().ToList();
                var sectionList = _sectionRepository.GetAll().ToList();
                var programList = _programRepository.GetAll().ToList();
                var companyList = _companyRepository.GetAll().ToList();
                var accountList = _accountRepository.GetAll().ToList();


                #endregion

                var mappedSubmissionList = new List<Submission>();
                foreach (var id in idList)
                {
                    mappedSubmissionList.Add(submissionList.First(s=>s.Id == id));
                }

                #region Body
                int index = 1;
                foreach (var submission in mappedSubmissionList)
                {
                    worksheet.Cell(1 + index, 1).Value = accountList.Find(s=>s.Id == submission.Student.AccountId).Email; //_accountRepository.Get(_studentRepository.Get(_submissionRepository.Get(submission.Id).StudentId).AccountId).Email;
                    worksheet.Cell(1 + index, 2).Value = submission.IsoCode;
                    worksheet.Cell(1 + index, 3).Value = submission.LastName;
                    worksheet.Cell(1 + index, 4).Value = submission.FirstName;
                    worksheet.Cell(1 + index, 5).Value = submission.MiddleInitial;
                    worksheet.Cell(1 + index, 6).Value = submission.StudentNumber;
                    worksheet.Cell(1 + index, 7).Value = sectionList.Find(s => s.Id == submission.Student.SectionId).Name;
                    worksheet.Cell(1 + index, 8).Value = programList.Find(p=>p.Id == submission.Student.ProgramId).Name;
                    worksheet.Cell(1 + index, 9).Value = programList.Find(p => p.Id == submission.Student.ProgramId).NumberOfHours;
                    worksheet.Cell(1 + index, 10).Value = submission.ContactPerson;
                    worksheet.Cell(1 + index, 11).Value = submission.ContactPersonEmail;
                    worksheet.Cell(1 + index, 12).Value = submission.ContactPersonPosition;
                    worksheet.Cell(1 + index, 13).Value = companyList.Find(c => c.Id == submission.CompanyId).Name;
                    worksheet.Cell(1 + index, 14).Value = companyList.Find(c => c.Id == submission.CompanyId).AddressOne;
                    worksheet.Cell(1 + index, 15).Value = companyList.Find(c => c.Id == submission.CompanyId).AddressTwo;
                    worksheet.Cell(1 + index, 16).Value = companyList.Find(c => c.Id == submission.CompanyId).AddressThree;
                    worksheet.Cell(1 + index, 17).Value = companyList.Find(c => c.Id == submission.CompanyId).City;
                    worksheet.Cell(1 + index, 18).Value = submission.SubmissionDate;
                    // worksheet.Cell(1 + index, 19).Value = 

                    #region Status Logic Value

                    if (submission.AdminResponse.AcceptedByCoordinator == null && submission.AdminResponse.AcceptedByChair == null)
                    {
                        worksheet.Cell(1 + index, 19).Value = "Request Submitted";
                    }
                    else if (submission.AdminResponse.AcceptedByCoordinator == true && submission.AdminResponse.AcceptedByChair == null)
                    {
                        worksheet.Cell(1 + index, 19).Value = "Accepted By Coordinator";
                    }
                    else if (submission.AdminResponse.AcceptedByChair == true && submission.AdminResponse.AcceptedByDean == null)
                    {
                        worksheet.Cell(1 + index, 19).Value = "Accepted By Chair";
                    }
                    else if (submission.AdminResponse.AcceptedByDean == true && submission.AdminResponse.EmailSentByCoordinator == null)
                    {
                        worksheet.Cell(1 + index, 19).Value = "Accepted By Dean";
                    }
                    else if (submission.AdminResponse.EmailSentByCoordinator == true && submission.AdminResponse.CompanyAgrees == null)
                    {
                        worksheet.Cell(1 + index, 19).Value = "Email sent to company rep";
                    }
                    else
                    {
                        worksheet.Cell(1 + index, 19).Value = "Student Accepted";
                    }

                    #endregion

                    worksheet.Cell(1 + index, 20).Value = $"fileloc/{submission.AcceptanceLetterFileName}";
                    worksheet.Cell(1 + index, 21).Value = $"fileloc/{submission.CompanyProfileFileName}";
                    index++;
                }


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
