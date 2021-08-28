using System.Linq;
using System.Net;
using System.Net.Mail;
using InternConnect.Data.Interfaces;

namespace InternConnect.Service.ThirdParty
{
    public interface IMailerService
    {
        public SmtpClient SmtpConfiguration();

        //when student submits -> coordinator
        public void NotifyCoordinator(int sectionId);

        //when coordinator approves -> updateStudent -> updateChair
        public void NotifyChair(int submissionId, int adminId);
        //when chair approves -> updateStudent -> updateDean
        public void NotifyDean(int submissionId);
        //when dean approves -> updateStudent -> updateIGAARP -> updateCoordinator
        public void NotifyCoordAndIgaarp(int submissionId);

        //when emailSent -> updateStudent
        public void NotifyStudentEmailSent(int submissionId);

        //when companyApproves -> updateStudent
        public void NotifyStudentCompanyApproves(int submissionId);

        //reset password


    }

    public class MailerService : IMailerService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IAcademicYearRepository _academicYearRepository;

        public MailerService(IAdminRepository adminRepository, IAccountRepository accountRepository,
            IStudentRepository studentRepository, ISubmissionRepository submissionRepository, IAcademicYearRepository academicYear)
        {
            _adminRepository = adminRepository;
            _accountRepository = accountRepository;
            _studentRepository = studentRepository;
            _submissionRepository = submissionRepository;
            _academicYearRepository = academicYear;

        }

        public SmtpClient SmtpConfiguration()
        {
            var client = new SmtpClient("mail.eco-tigers.com", 8889);
            client.EnableSsl = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("postmaster@eco-tigers.com", "landayan24");
            return client;
        }

        public void NotifyCoordinator(int sectionId)
        {
            var adminData = _adminRepository.Find(a => a.SectionId == sectionId).First();
            var accountData = _accountRepository.Get(adminData.AccountId);


            //mailer
            var client = SmtpConfiguration();
            var msg = new MailMessage();
            msg.To.Add(accountData.Email);
            msg.From = new MailAddress("postmaster@eco-tigers.com");
            msg.Subject = "Student Submission For Letter";
            msg.Body = "A student has submitted a form. Check it here";
            client.Send(msg);
        }

        public void NotifyChair(int submissionId, int adminId)
        {
            var client = SmtpConfiguration();
            var coordinatorData = _adminRepository.Get(adminId);
            var chairData = _adminRepository.Find(a => a.AuthId == 2 && a.ProgramId == coordinatorData.ProgramId)
                .First();
            var accountData = _accountRepository.Get(chairData.AccountId);


            //Mailer
            var toChair = new MailMessage();
            toChair.To.Add(accountData.Email);
            toChair.From = new MailAddress("postmaster@eco-tigers.com");
            toChair.Subject = "Student Submission For Letter";
            toChair.Body = "A coordinator has verified a submission. Check it here";
            client.Send(toChair);


            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountDataStudent = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountDataStudent.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Endorsement Letter Request";
            toStudent.Body = "Coordinator has updated your request";
            client.Send(toStudent);
        }

        public void NotifyDean(int submissionId)
        {
            var client = SmtpConfiguration();
            var deanData = _adminRepository.Find(a => a.AuthId == 1)
                .First();
            var accountData = _accountRepository.Get(deanData.AccountId);

            var toDean = new MailMessage();
            toDean.To.Add(accountData.Email);
            toDean.From = new MailAddress("postmaster@eco-tigers.com");
            toDean.Subject = "Student Submission For Letter";
            toDean.Body = "Send to dean";
            client.Send(toDean);

            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountDataStudent = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountDataStudent.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Endorsement Letter Request";
            toStudent.Body = "Chair has updated your request";
            client.Send(toStudent);
        }

        public void NotifyCoordAndIgaarp(int submissionId)
        {
            var client = SmtpConfiguration();

            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var coordinatorData = _adminRepository.Find(a => a.SectionId == studentData.SectionId).First();
            var accountData = _accountRepository.Get(coordinatorData.AccountId);

            var toCoordinator = new MailMessage();
            toCoordinator.To.Add(accountData.Email);
            toCoordinator.From = new MailAddress("postmaster@eco-tigers.com");
            toCoordinator.Subject = "Student Submission For Letter";
            toCoordinator.Body = "Send to coordinator";
            client.Send(toCoordinator);

            var ayData = _academicYearRepository.GetAll().First();
            var toIgaarp = new MailMessage();
            toIgaarp.To.Add(ayData.IgaarpEmail);
            toIgaarp.From = new MailAddress("postmaster@eco-tigers.com");
            toIgaarp.Subject = "Student Submission For Letter";
            toIgaarp.Body = "Send to Igaarp";
            client.Send(toIgaarp);

            var accountDataStudent = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountDataStudent.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Student Submission For Letter";
            toStudent.Body = "Send to Student";
            client.Send(toStudent);

        }

        public void NotifyStudentEmailSent(int submissionId)
        {
            var client = SmtpConfiguration();
            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountData = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountData.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Student Submission For Letter";
            toStudent.Body = "Send to Student second to last step";
            client.Send(toStudent);

        }

        public void NotifyStudentCompanyApproves(int submissionId)
        {
            var client = SmtpConfiguration();
            var submissionData = _submissionRepository.Get(submissionId);
            var studentData = _studentRepository.Get(submissionData.StudentId);
            var accountData = _accountRepository.Get(studentData.AccountId);
            var toStudent = new MailMessage();
            toStudent.To.Add(accountData.Email);
            toStudent.From = new MailAddress("postmaster@eco-tigers.com");
            toStudent.Subject = "Student Submission For Letter";
            toStudent.Body = "Send to Student last step";
            client.Send(toStudent);
        }
    }
}