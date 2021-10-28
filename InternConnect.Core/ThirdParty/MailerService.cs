using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Event;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace InternConnect.Service.ThirdParty
{
    public interface IMailerService
    {

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

        public void NotifyStudentEvent(List<Student> studentList, EventDto.AddEvent payload);
    }

    public class MailerService : IMailerService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IPdfService _pdfService;
        private readonly IAdminResponseRepository _adminResponseRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public MailerService(IAdminRepository adminRepository, ISubmissionRepository submissionRepository,
            IAcademicYearRepository academicYear, IConfiguration configuration, IWebHostEnvironment environment,
            IPdfService pdfService, IAdminResponseRepository adminResponse)
        {
            _adminRepository = adminRepository;
            _submissionRepository = submissionRepository;
            _academicYearRepository = academicYear;
            _configuration = configuration;
            _env = environment;
            _pdfService = pdfService;
            _adminResponseRepository = adminResponse;
        }

        //private SmtpClient SmtpConfiguration(int authId)
        //{
        //    var client = new SmtpClient("mail5015.site4now.net", 8889);
        //    client.EnableSsl = false;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    if (authId == 1)
        //    {
        //        client.Credentials = new NetworkCredential("dean-noreply@internconnect-cics.com", "internconnect!0!");
        //    }
        //    else if (authId == 2)
        //    {
        //        client.Credentials = new NetworkCredential("chair-noreply@internconnect-cics.com", "internconnect!0!");
        //    }
        //    else if (authId == 3)
        //    {
        //        client.Credentials = new NetworkCredential("coordinator-noreply@internconnect-cics.com", "internconnect!0!");
        //    }
        //    else if (authId == 4)
        //    {
        //        client.Credentials = new NetworkCredential("postmaster-noreply@internconnect-cics.com", "internconnect!0!");
        //    }


        //    return client;
        //}


        public void NotifyCoordinator(int sectionId)
        {
            var adminData = _adminRepository.GetAllAdminsWithRelatedData().First(a => a.SectionId == sectionId);
            var mailText = ReadHtml("submission-new");
            mailText = mailText.Replace("[new-submission]",
                $"{_configuration["ClientAppUrl"]}/admin/new-submissions");
            SendMailToApi(adminData.Account.Email, mailText, "You have a new endorsement request").Content.ToString();
        }

        public void NotifyChair(int submissionId, int adminId, bool isAccepted)
        {
            var mailToAdmin = ReadHtml("submission-final");
            mailToAdmin = mailToAdmin.Replace("[final-submission]",
                $"{_configuration["ClientAppUrl"]}/admin/final-submissions");

            var mailToStudent = ReadHtml("status-acknowledged");

            var failText = ReadHtml("status-disapproved");
            failText = failText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");

            var coordinatorData = _adminRepository.GetAllAdminsWithRelatedData().First(a => a.Id == adminId);
            var chairData = _adminRepository.GetAllAdminsWithRelatedData()
                .First(a => a.AuthId == 2 && a.ProgramId == coordinatorData.ProgramId);
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            var adminResponses = _adminResponseRepository.GetAll().Where(a =>
                a.AcceptedByCoordinator == true && a.AcceptedByChair == null).ToList();

            if (isAccepted)
            {
                if (adminResponses.Count == 10) SendMailToApi(chairData.Account.Email, mailToAdmin, "You currently have a lot of requests today").Content.ToString();
                SendMailToApi(submissionData.Student.Account.Email, mailToStudent, "Some Additional Updates").Content.ToString();
            }
            else
            {
                SendMailToApi(submissionData.Student.Account.Email, failText, "Sorry, your request was disapproved").Content.ToString();
            }
        }

        public void NotifyDean(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("submission-final");
            mailText = mailText.Replace("[final-submission]",
                $"{_configuration["ClientAppUrl"]}/admin/pending-submissions");

            var mailToStudent = ReadHtml("status-acknowledged-w-chair");

            var failText = ReadHtml("status-disapproved");
            failText = failText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");

            var deanData = _adminRepository.GetAllAdminsWithRelatedData().First(a => a.AuthId == 1);
            var responseData = _adminResponseRepository.GetAll().Where(s =>
                s.AcceptedByChair == true && s.AcceptedByDean == null).ToList();
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);

            if (isAccepted)
            {
                if (responseData.Count == 10)
                {
                    SendMailToApi(deanData.Account.Email, mailText, "You currently have a lot of requests today").Content.ToString();
                }
                SendMailToApi(submissionData.Student.Account.Email, mailToStudent, "Some Additional Updates").Content.ToString();
            }
            else
            {
                SendMailToApi(submissionData.Student.Account.Email, failText, "Sorry, your request was disapproved").Content.ToString();
            }
            
        }

        public void NotifyCoordAndIgaarp(int submissionId, bool isAccepted)
        {
            var mailToAdmin = ReadHtml("submission-pending");
            mailToAdmin = mailToAdmin.Replace("[pending-submission]",
                $"{_configuration["ClientAppUrl"]}/admin/pending-submissions");

            var mailToStudent = ReadHtml("status-signed");
            mailToStudent = mailToStudent.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");

            var mailToIgaarp = ReadHtml("submission-igaarp");

            var failText = ReadHtml("status-disapproved");
            failText = failText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");

            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            var coordinatorData = _adminRepository.GetAllAdminsWithRelatedData()
                .First(a => a.SectionId == submissionData.Student.SectionId);
            var accountData = coordinatorData.Account;
            var accountDataStudent = submissionData.Student.Account;


            if (isAccepted)
            {
                SendMailToApi(accountData.Email, mailToAdmin, "You've received an endorsement letter from the Dean").Content.ToString();
                #region MailToIgaarp
                var content = _pdfService.AddPdf(submissionId).ToArray();
                var ayData = _academicYearRepository.GetAll().First();
                //var toIgaarp = new MailMessage();
                //toIgaarp.To.Add(ayData.IgaarpEmail);
                //toIgaarp.From = new MailAddress("dean-noreply@internconnect-cics.com");
                //toIgaarp.Subject = "The CICS Dean recently signed an endorsement letter";
                //toIgaarp.Body = mailToIgaarp;
                //toIgaarp.Attachments.Add(new Attachment(new MemoryStream(content),
                //    $"{submissionData.Student.Program.Name}_{submissionData.LastName}, {submissionData.FirstName} {submissionData.LastName} {submissionData.MiddleInitial}.pdf"));
                //toIgaarp.IsBodyHtml = true;
                //client.Send(toIgaarp);
                SendMailToApiWithAttachment(ayData.IgaarpEmail, mailToIgaarp,
                        "The CICS Dean recently signed an endorsement letter", new MemoryStream(content),
                        submissionData)
                    .Content.ToString();

                #endregion
            }

            SendMailToApi(accountDataStudent.Email, isAccepted ? mailToStudent : failText, isAccepted ? "Your endorsement letter is signed" : "Sorry, your request was disapproved");
        }

        public void NotifyStudentEmailSent(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("status-senttocompany");
            mailText = mailText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");
            var failText = ReadHtml("status-disapproved");
            failText = failText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            SendMailToApi(submissionData.Student.Account.Email, isAccepted ? mailText : failText,
                isAccepted ? "Your endorsement letter has been sent to the company" : "Sorry, your request was disapproved");
        }

        public void NotifyStudentCompanyApproves(int submissionId, bool isAccepted)
        {
            var mailText = ReadHtml("status-accepted");
            mailText = mailText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");
            var failText = ReadHtml("status-disapproved");
            failText = failText.Replace("[view-status]",
                $"{_configuration["ClientAppUrl"]}/status");
            var submissionData = _submissionRepository.GetAllRelatedData().First(s => s.Id == submissionId);
            SendMailToApi(submissionData.Student.Account.Email, isAccepted ? mailText : failText,
                isAccepted? "Congrats! Your endorsement is accepted": "Sorry, your request was disapproved");
        }

        public void ForgotPassword(Account accountData)
        {

            var mailText = ReadHtml("resetpassword");
            mailText = mailText.Replace("[forgotpassword]",
                $"{_configuration["ClientAppUrl"]}" +
                $"/forgotpassword?email={accountData.Email}&resetkey={accountData.ResetKey}");
            SendMailToApi(accountData.Email, mailText, "Password Reset Information").Content.ToString();
        }

        public void ChangeDean(string oldEmail, string newEmail, string resetkey)
        {
            var message = $"{_configuration["ClientAppUrl"]}/" +
                          $"changedean?oldemail={oldEmail}&newemail={newEmail}&resetkey={resetkey}";
            var mailText = ReadHtml("onboarding");
            mailText = mailText.Replace("[onboarding]", message);
            SendMailToApi(newEmail, mailText, "Welcome to InternConnect!");
        }

        public void Onboard(Account accountData)
        {
            var message = $"{_configuration["ClientAppUrl"]}/" +
                          $"onboard?email={accountData.Email}&resetkey={accountData.ResetKey}";
            var mailText = ReadHtml("onboarding");
            mailText = mailText.Replace("[onboarding]", message);
            SendMailToApi(accountData.Email, mailText, "Welcome to InternConnect!");
        }

        public void NotifyStudentEvent(List<Student> studentList, EventDto.AddEvent payload)
        {
            //var message
            var endDate = DateTime.ParseExact(payload.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMMM dd, yyyy");
            string template;
                var mailText = ReadHtml("events");
            template = mailText.Replace("[[-insert-event-name-here]]", payload.Name);
            template = template.Replace("[MM-DD-YYYY]", endDate);
            foreach (var student in studentList)
            {
                SendMailToApi(student.Account.Email, template, "Event reminder").Content.ToString();
            }

        }


        private string ReadHtml(string fileName)
        {
            var str = new StreamReader(_env.ContentRootPath + "/resources/email/" + fileName + ".html");
            var mailText = str.ReadToEnd();
            str.Close();

            return mailText;
        }



        private IRestResponse SendMailToApi(string email, string payload, string subject)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/internconnect-cics.com");

            client.Authenticator = new HttpBasicAuthenticator("api",
                "key-6b82a3acac6564095c9dac25eb3e2e09");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "internconnect-cics.com", ParameterType.UrlSegment);
            request.Resource = "https://api.mailgun.net/v3/internconnect-cics.com/messages";
            request.AddParameter("from", "postmaster-noreply<mailgun@internconnect-cics.com>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("html", payload);
            request.Method = Method.POST;
            return client.Execute(request);
        }
        

        private IRestResponse SendMailToApiWithAttachment(string email, string payload, string subject, MemoryStream content, Submission submissionData)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/internconnect-cics.com");

            client.Authenticator = new HttpBasicAuthenticator("api",
                "67bcb6bd8d580970b4017b5511acf438-20ebde82-4078e5bb");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "internconnect-cics.com", ParameterType.UrlSegment);
            request.Resource = "https://api.mailgun.net/v3/internconnect-cics.com/messages";
            request.AddParameter("from", "postmaster-noreply<mailgun@internconnect-cics.com>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("html", payload);
            request.AddFile("attachment",content.ToArray(),
                $"{submissionData.Student.Program.Name}_{submissionData.LastName}, {submissionData.FirstName} {submissionData.LastName} {submissionData.MiddleInitial}.pdf");

            request.Method = Method.POST;
            return client.Execute(request);
        }

        //private IRestResponse SendMailToMultiplePeople(string payload, string subject, List<Student> studentList)
        //{
        //    Dictionary<string, Dictionary<string, string>> recipientVariables =
        //        new Dictionary<string, Dictionary<string, string>>();

        //    RestClient client = new RestClient();
        //    client.BaseUrl = new Uri("https://api.mailgun.net/v3/internconnect-cics.com");

        //    client.Authenticator = new HttpBasicAuthenticator("api",
        //        "key-6b82a3acac6564095c9dac25eb3e2e09");
        //    RestRequest request = new RestRequest();
        //    request.AddParameter("domain", "internconnect-cics.com", ParameterType.UrlSegment);
        //    request.Resource = "https://api.mailgun.net/v3/internconnect-cics.com/messages";
        //    request.AddParameter("from", "Excited User <mailgun@internconnect-cics.com>");
        //    foreach (var student in studentList)
        //    {
        //        request.AddParameter("to", student.Account.Email);
        //        recipientVariables.Add(student.Account.Email,new Dictionary<string, string>
        //        {
        //            {"unique_id",student.Account.Id.ToString()}
        //        });
        //    }


        //    var json = JsonConvert.SerializeObject(recipientVariables);

        //    request.AddParameter("subject", subject);
        //    request.AddParameter("html", payload);
        //    request.AddParameter("recipient-variables", json);

        //    request.Method = Method.POST;
        //    return client.Execute(request);
        //}


        //private void SendMail(string email, string emailTemplate, string subject, int authId)
        //{
        //    var client = SmtpConfiguration(authId);
        //    var mail = new MailMessage();
        //    mail.To.Add(email);
        //    mail.From = new MailAddress(
        //        authId == 1? "dean-noreply@internconnect-cics.com":
        //        authId == 2? "chair-noreply@internconnect-cics.com":
        //        authId == 3? "coordinator-noreply@internconnect-cics.com":
        //        "postmaster-noreply@internconnect-cics.com");
        //    mail.Subject = subject;
        //    mail.Body = emailTemplate;
        //    mail.IsBodyHtml = true;
        //    client.Send(mail);
        //}

        //    client.Credentials = new NetworkCredential("dean-noreply@internconnect-cics.com", "internconnect!0!");
        //}
        //else if (authId == 2)
        //{
        //client.Credentials = new NetworkCredential("chair-noreply@internconnect-cics.com", "internconnect!0!");
        //}
        //else if (authId == 3)
        //{
        //client.Credentials = new NetworkCredential("coordinator-noreply@internconnect-cics.com", "internconnect!0!");
        //}
        //else if (authId == 4)
        //{
        //client.Credentials = new NetworkCredential("postmaster@internconnect-cics.com", "internconnect!0!");

    }
}