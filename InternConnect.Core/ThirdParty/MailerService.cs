using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace InternConnect.Service.ThirdParty
{
    public interface IMailerService
    {
        public SmtpClient SmtpConfiguration();

        //when student submits -> coordinator
        public void NotifyCoordinator(int sectionId);

        //when coordinator approves -> updateStudent -> updateChair
        public void NotifyChair(int submissionId, int adminId, bool isAccepted);

        //when chair approves -> updateStudent -> updateDean
        public void NotifyDean(int submissionId, bool isAccepted);

        //when dean approves -> updateStudent -> updateIGAARP -> updateCoordinator
        public void NotifyCoordAndIgaarp(int submissionId, bool isAccepted);

        //when emailSent -> updateStudent
        public void NotifyStudentEmailSent(int submissionId, bool isAccepted);

        //when companyApproves -> updateStudent
        public void NotifyStudentCompanyApproves(int submissionId, bool isAccepted);

        //reset password
        public void ForgotPassword(Account accountData);

        //Onboard
        public void Onboard(Account accountData);

        public void ChangeDean(string oldEmail, string newEmail, string resetkey);
    }

    public class MailerService : IMailerService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IPdfService _pdfService;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public MailerService(IAdminRepository adminRepository, IAccountRepository accountRepository,
            IStudentRepository studentRepository, ISubmissionRepository submissionRepository,
            IAcademicYearRepository academicYear, IConfiguration configuration, IWebHostEnvironment environment,
            IPdfService pdfService)
        {
            _adminRepository = adminRepository;
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _submissionRepository = submissionRepository;
            _academicYearRepository = academicYear;
            _configuration = configuration;
            _env = environment;
            _pdfService = pdfService;
        }

        public SmtpClient SmtpConfiguration()
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("internconnectsmtp@gmail.com", "internconnect101");
            return client;
        }


        public void NotifyCoordinator(int sectionId)
        {
            var adminData = _adminRepository.Find(a => a.SectionId == sectionId).First();
            var accountData = _accountRepository.Get(adminData.AccountId);

            var mailText = ReadHtml("submission-new");

            //mailer
            var client = SmtpConfiguration();
            var msg = new MailMessage();
            msg.To.Add(accountData.Email);
            msg.From = new MailAddress("postmaster@eco-tigers.com");
            msg.Subject = "Student Submission For Letter";
            msg.Body = mailText;
            msg.IsBodyHtml = true;
            client.Send(msg);
        }

        public void NotifyChair(int submissionId, int adminId, bool isAccepted)
        {
            var mailToAdmin = ReadHtml("submission-final");
            var failText = ReadHtml("status-disapproved");


            var client = SmtpConfiguration();
            var coordinatorData = _adminRepository.Get(adminId);
            var chairData = _adminRepository.Find(a => a.AuthId == 2 && a.ProgramId == coordinatorData.ProgramId)
                .First();
            var accountData = _accountRepository.Get(chairData.AccountId);
            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountDataStudent = _accountRepository.Get(studentData.AccountId);
            var adminResponses = _submissionRepository.GetAllRelatedData().Where(s =>
                s.AdminResponse.AcceptedByCoordinator == true && s.AdminResponse.AcceptedByChair == null).ToList();

            var toChair = new MailMessage();
            var toStudent = new MailMessage();

            if (isAccepted)
            {
                if (adminResponses.Count == 10)
                {
                    toChair.To.Add(accountData.Email);
                    toChair.From = new MailAddress("postmaster@eco-tigers.com");
                    toChair.Subject = "Student Submission For Letter";
                    toChair.Body = mailToAdmin;
                    toChair.IsBodyHtml = true;
                    client.Send(toChair);
                }
            }

            else
            {
                toStudent.To.Add(accountDataStudent.Email);
                toStudent.From = new MailAddress("postmaster@eco-tigers.com");
                toStudent.Subject = "Endorsement Letter Request";
                toStudent.Body = failText;
                toStudent.IsBodyHtml = true;
                client.Send(toStudent);
            }


            //Mailer
        }

        public void NotifyDean(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("submission-final");
            var failText = ReadHtml("status-disapproved");
            var client = SmtpConfiguration();
            var deanData = _adminRepository.Find(a => a.AuthId == 1)
                .First();
            var accountData = _accountRepository.Get(deanData.AccountId);
            var responseData = _submissionRepository.GetAllRelatedData().Where(s =>
                s.AdminResponse.AcceptedByChair == true && s.AdminResponse.AcceptedByDean == null).ToList();
            if (isAccepted)
            {
                if (responseData.Count == 10)
                {
                    var toDean = new MailMessage();
                    toDean.To.Add(accountData.Email);
                    toDean.From = new MailAddress("postmaster@eco-tigers.com");
                    toDean.Subject = "Student Submission For Letter";
                    toDean.Body = mailText;
                    toDean.IsBodyHtml = true;
                    client.Send(toDean);
                }
            }

            else
            {
                var submissionData = _submissionRepository.Get(submissionId);
                var studentData = _studentRepository.Get(submissionData.StudentId);
                var accountDataStudent = _accountRepository.Get(studentData.AccountId);
                var toStudent = new MailMessage();
                toStudent.To.Add(accountDataStudent.Email);
                toStudent.From = new MailAddress("postmaster@eco-tigers.com");
                toStudent.Subject = "Endorsement Letter Request Update";
                toStudent.Body = failText;
                toStudent.IsBodyHtml = true;
                client.Send(toStudent);
            }
        }

        public void NotifyCoordAndIgaarp(int submissionId, bool isAccepted)
        {
            var mailToAdmin = ReadHtml("submission-pending");
            var mailToStudent = ReadHtml("status-signed");
            var mailToIgaarp = ReadHtml("submission-igaarp");
            var failText = ReadHtml("status-disapproved");

            var client = SmtpConfiguration();
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            var studentData = submissionData.Student;
            var coordinatorData = _adminRepository.GetAllAdminsWithRelatedData()
                .First(a => a.SectionId == studentData.SectionId);
            var accountData = coordinatorData.Account;
            var accountDataStudent = studentData.Account;
            if (isAccepted)
            {
                var toCoordinator = new MailMessage();
                toCoordinator.To.Add(accountData.Email);
                toCoordinator.From = new MailAddress("postmaster@eco-tigers.com");
                toCoordinator.Subject = "Student Submission For Letter";
                toCoordinator.Body = mailToAdmin;
                toCoordinator.IsBodyHtml = true;
                client.Send(toCoordinator);

                var content = _pdfService.AddPdf(submissionId).ToArray();

                var ayData = _academicYearRepository.GetAll().First();
                var toIgaarp = new MailMessage();
                toIgaarp.To.Add(ayData.IgaarpEmail);
                toIgaarp.From = new MailAddress("postmaster@eco-tigers.com");
                toIgaarp.Subject = "Student Submission For Letter";
                toIgaarp.Body = mailToIgaarp;
                toIgaarp.Attachments.Add(new Attachment(new MemoryStream(content),
                    $"{submissionData.StudentNumber}.pdf"));
                toIgaarp.IsBodyHtml = true;
                client.Send(toIgaarp);

                var toStudent = new MailMessage();
                toStudent.To.Add(accountDataStudent.Email);
                toStudent.From = new MailAddress("postmaster@eco-tigers.com");
                toStudent.Subject = "Student Submission For Letter";
                toStudent.Body = mailToStudent;
                toStudent.IsBodyHtml = true;
                client.Send(toStudent);
            }
            else
            {
                var toStudent = new MailMessage();
                toStudent.To.Add(accountDataStudent.Email);
                toStudent.From = new MailAddress("postmaster@eco-tigers.com");
                toStudent.Subject = "Student Submission For Letter";
                toStudent.Body = failText;
                toStudent.IsBodyHtml = true;
                client.Send(toStudent);
            }
        }

        public void NotifyStudentEmailSent(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("status-senttocompany");
            var failText = ReadHtml("status-disapproved");
            var client = SmtpConfiguration();
            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountData = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountData.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Student Submission For Letter";
            toStudent.Body = isAccepted? mailText:failText;
            toStudent.IsBodyHtml = true;
            client.Send(toStudent);
        }

        public void NotifyStudentCompanyApproves(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("status-accepted");
            var failText = ReadHtml("status-disapproved");
            var client = SmtpConfiguration();

            var submissionData = _submissionRepository.Get(submissionId);
                var studentData = _studentRepository.Get(submissionData.StudentId);
                var accountData = _accountRepository.Get(studentData.AccountId);
                var toStudent = new MailMessage();
                toStudent.To.Add(accountData.Email);
                toStudent.From = new MailAddress("postmaster@eco-tigers.com");
                toStudent.Subject = "Student Submission For Letter";
                toStudent.Body = isAccepted ? mailText:failText;
                toStudent.IsBodyHtml = true;
                client.Send(toStudent);



            
            

        }

        public void ForgotPassword(Account accountData)
        {
            var mailText = ReadHtml("resetpassword");
            mailText = mailText.Replace("[forgotpassword]",
                $"{_configuration["ClientAppUrl"]}" +
                $"/forgotpassword?email={accountData.Email}&resetkey={accountData.ResetKey}");

            //var message = $"{_configuration["ClientAppUrl"]}" + $"/login?email={accountData.Email}&resetkey={accountData.ResetKey}";
            var client = SmtpConfiguration();
            var toAccount = new MailMessage();
            toAccount.To.Add(accountData.Email);
            toAccount.From = new MailAddress("internconnectsmtp@gmail.com");
            toAccount.Subject = "Reset Password Link";
            toAccount.Body = mailText;
            toAccount.IsBodyHtml = true;
            client.Send(toAccount);
        }

        public void ChangeDean(string oldEmail, string newEmail, string resetkey)
        {
            var message = $"{_configuration["ClientAppUrl"]}/" +
                          $"changedean?oldemail={oldEmail}&newemail={newEmail}&resetkey={resetkey}";
            var mailText = ReadHtml("onboarding");

            mailText = mailText.Replace("[onboarding]", message);


            var client = SmtpConfiguration();
            var toAccount = new MailMessage();
            toAccount.To.Add(newEmail);
            toAccount.From = new MailAddress("postmaster@eco-tigers.com");
            toAccount.Subject = "Reset Password Link";
            toAccount.Body = mailText;
            toAccount.IsBodyHtml = true;

            client.Send(toAccount);
        }

        public void Onboard(Account accountData)
        {
            var message = $"{_configuration["ClientAppUrl"]}/" +
                          $"onboard?email={accountData.Email}&resetkey={accountData.ResetKey}";

            var mailText = ReadHtml("onboarding");

            mailText = mailText.Replace("[onboarding]", message);

            var client = SmtpConfiguration();
            var toAccount = new MailMessage();
            toAccount.To.Add(accountData.Email);
            toAccount.From = new MailAddress("postmaster@eco-tigers.com");
            toAccount.Subject = "Reset Password Link";
            toAccount.Body = mailText;
            toAccount.IsBodyHtml = true;
            client.Send(toAccount);
        }


        private string ReadHtml(string fileName)
        {
            var str = new StreamReader(_env.ContentRootPath + "/resources/email/" + fileName + ".html");
            var mailText = str.ReadToEnd();
            str.Close();

            return mailText;
        }
    }
}