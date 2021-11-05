using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.Company;
using InternConnect.Dto.Submission;
using InternConnect.Service.ThirdParty;

namespace InternConnect.Service.Main
{
    public interface ISubmissionService
    {
        public SubmissionDto.ReadSubmission AddSubmission(SubmissionDto.AddSubmission payload, int sectionId,
            int programId);

        public void UpdateSubmission(SubmissionDto.UpdateSubmission payload);
        public SubmissionDto.ReadSubmission GetSubmission(int id);
        public IEnumerable<SubmissionDto.SubmissionReports> GetAllSubmissions();
        public IEnumerable<SubmissionDto.SubmissionReports> GetSubmissionBySection(int sectionId);
        public IEnumerable<SubmissionDto.SubmissionReports> GetSubmissionByProgram(int programId);
        public IEnumerable<SubmissionDto.SubmissionStatus> GetSubmissionsByStep(int stepNumber, int uniqueId);
        public IEnumerable<CompanyAndNumberOfStudentModel> GetSubmissionByNumberOfCompanyOccurence(string type, int id);
    }

    public class SubmissionService : ISubmissionService
    {
        private readonly IAdminResponseRepository _adminResponse;
        private readonly ICompanyRepository _companyRepository;
        private readonly InternConnectContext _context;
        private readonly ILogsRepository _logsRepository;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;
        private readonly IProgramService _programService;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionService(ISubmissionRepository submission, IMapper mapper,
            InternConnectContext context, IMailerService mailerService,
            IProgramService programService,
            ICompanyRepository companyRepository, ILogsRepository logsRepository, IStudentRepository studentRepository,
            IAdminResponseRepository adminResponse)
        {
            _mapper = mapper;
            _context = context;
            _submissionRepository = submission;

            _mailerService = mailerService;

            _programService = programService;
            _companyRepository = companyRepository;
            _logsRepository = logsRepository;
            _studentRepository = studentRepository;
            _adminResponse = adminResponse;
        }

        public SubmissionDto.ReadSubmission AddSubmission(SubmissionDto.AddSubmission payload, int sectionId,
            int programId)
        {
            var submissionData = _mapper.Map<Submission>(payload);
            var studentProgram = _programService.GetById(programId);
            if (studentProgram.NumberOfHours == null ||
                studentProgram.IsoCodeProgramNumber == null) return new SubmissionDto.ReadSubmission();
            var adminResponse = new AdminResponse();
            adminResponse.Comments = null;
            submissionData.AdminResponse = adminResponse;
            submissionData.SubmissionDate = GetDate();
            _submissionRepository.Add(submissionData);
            _context.SaveChanges();

            var studentData = _studentRepository.GetStudentWithAccountData(payload.StudentId);
            _logsRepository.Add(new Logs
                {
                    Action =
                        $"{studentData.Account.Email} CREATED A SUBMISSION",
                    DateStamped = GetDate(),
                    SubmissionId = submissionData.Id,
                    ActorEmail = studentData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(studentData.AuthId).Name
                }
            );
            _context.SaveChanges();

            _mailerService.NotifyCoordinator(sectionId);
            return _mapper.Map<SubmissionDto.ReadSubmission>(submissionData);
        }

        public IEnumerable<SubmissionDto.SubmissionReports> GetAllSubmissions()
        {
            var submissionList = _submissionRepository.GetAllRelatedData();
            var mappedList = GetSubmissionReports(submissionList);
            return mappedList;
        }

        public SubmissionDto.ReadSubmission GetSubmission(int studentId)
        {
            return _mapper.Map<SubmissionDto.ReadSubmission>(_submissionRepository.GetAllRelatedData().ToList()
                .Last(s => s.StudentId == studentId));
        }

        public IEnumerable<CompanyAndNumberOfStudentModel> GetSubmissionByNumberOfCompanyOccurence(string type, int id)
        {
            var submissionList = _submissionRepository.GetAllRelatedData();

            if (type == "whole" && id == 0)
                return submissionList.GroupBy(x => x.CompanyId)
                    .Select(x => new CompanyAndNumberOfStudentModel
                        { CompanyName = _companyRepository.Get(x.Key).Name, NumberOfOccurence = x.Count() })
                    .OrderByDescending(c => c.NumberOfOccurence).ToList();

            if (type == "program")
                return submissionList.Where(s => s.Student.ProgramId == id).GroupBy(x => x.CompanyId)
                    .Select(x => new CompanyAndNumberOfStudentModel
                        { CompanyName = _companyRepository.Get(x.Key).Name, NumberOfOccurence = x.Count() })
                    .OrderByDescending(c => c.NumberOfOccurence).ToList();

            if (type == "section")
                return submissionList.Where(s => s.Student.SectionId == id).GroupBy(x => x.CompanyId)
                    .Select(x => new CompanyAndNumberOfStudentModel
                        { CompanyName = _companyRepository.Get(x.Key).Name, NumberOfOccurence = x.Count() })
                    .OrderByDescending(c => c.NumberOfOccurence).ToList();

            return null;
        }

        public IEnumerable<SubmissionDto.SubmissionReports> GetSubmissionByProgram(int programId)
        {
            var submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.ProgramId == programId);
            var mappedList = GetSubmissionReports(submissionList);

            return mappedList;
        }

        public IEnumerable<SubmissionDto.SubmissionReports> GetSubmissionBySection(int sectionId)
        {
            var submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.SectionId == sectionId);

            var mappedList = GetSubmissionReports(submissionList);

            return mappedList;
        }

        public IEnumerable<SubmissionDto.SubmissionStatus> GetSubmissionsByStep(int stepNumber, int uniqueId)
        {
            var submissionList = new List<Submission>();

            if (stepNumber == 1)
                submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.SectionId == uniqueId)
                    .ToList()
                    .FindAll(s => s.AdminResponse.AcceptedByCoordinator == null &&
                                  s.AdminResponse.AcceptedByChair == null);
            else if (stepNumber == 2)
                submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.ProgramId == uniqueId)
                    .ToList()
                    .FindAll(s =>
                        s.AdminResponse.AcceptedByCoordinator == true && s.AdminResponse.AcceptedByChair == null);
            else if (stepNumber == 3)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s => s.AdminResponse.AcceptedByChair == true && s.AdminResponse.AcceptedByDean == null);
            else if (stepNumber == 4)
                submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.SectionId == uniqueId)
                    .ToList()
                    .FindAll(s =>
                        s.AdminResponse.AcceptedByDean == true && s.AdminResponse.EmailSentByCoordinator == null);
            else if (stepNumber == 5)
                submissionList = _submissionRepository.GetAllRelatedData().Where(s => s.Student.SectionId == uniqueId)
                    .ToList()
                    .FindAll(s =>
                        s.AdminResponse.EmailSentByCoordinator == true && s.AdminResponse.CompanyAgrees == null);


            var mappedList = GetSubmissionStatuses(submissionList);
            return mappedList;
        }

        public void UpdateSubmission(SubmissionDto.UpdateSubmission payload)
        {
            var submissionData = _submissionRepository.GetAllRelatedData().Last(s => s.Id == payload.Id);
            submissionData.AdminResponse.AcceptedByChair = null;
            submissionData.AdminResponse.AcceptedByCoordinator = null;
            submissionData.AdminResponse.AcceptedByDean = null;
            submissionData.AdminResponse.Comments = null;
            submissionData.AdminResponse.CompanyAgrees = null;
            submissionData.AdminResponse.EmailSentByCoordinator = null;


            _mapper.Map(payload, submissionData);
            _context.SaveChanges();

            var studentData = _studentRepository.GetStudentWithAccountData(submissionData.StudentId);
            _logsRepository.Add(new Logs
                {
                    Action =
                        $"{studentData.Account.Email} SUBMITTED AN UPDATED SUBMISSION",
                    DateStamped = GetDate(),
                    SubmissionId = submissionData.Id,
                    ActorEmail = studentData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(studentData.AuthId).Name
                }
            );
            _context.SaveChanges();
        }


        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now,
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }

        private string GetSubmissionStatus(AdminResponse payload)
        {
            if (payload.AcceptedByCoordinator == false || payload.AcceptedByChair == false ||
                payload.AcceptedByDean == false || payload.EmailSentByCoordinator == false ||
                payload.CompanyAgrees == false)
                return "REJECTED";
            if (payload.AcceptedByCoordinator == null && payload.AcceptedByChair == null)
                return Status.StatusList.NEW_SUBMISSION.ToString();
            if (payload.AcceptedByCoordinator == true && payload.AcceptedByChair == null)
                return Status.StatusList.ACCEPTEDBYCOORDINATOR.ToString();
            if (payload.AcceptedByChair == true && payload.AcceptedByDean == null)
                return Status.StatusList.ACCEPTEDBYCHAIR.ToString();
            if (payload.AcceptedByDean == true && payload.EmailSentByCoordinator == null)
                return Status.StatusList.ACCEPTEDBYDEAN.ToString();
            if (payload.EmailSentByCoordinator == true && payload.CompanyAgrees == null)
                return Status.StatusList.EMAILSENTTOCOMPANY.ToString();
            if (payload.CompanyAgrees == true)
                return Status.StatusList.COMPANYAGREES.ToString();
            return "Unknown error";
        }

        private List<SubmissionDto.SubmissionReports> GetSubmissionReports(IEnumerable<Submission> payload)
        {
            var mappedList = new List<SubmissionDto.SubmissionReports>();
            var companyList = _companyRepository.GetAll().ToList();
            foreach (var submission in payload)
                mappedList.Add(new SubmissionDto.SubmissionReports
                {
                    CompanyName = companyList.First(s => s.Id == submission.CompanyId).Name,
                    ContactPersonEmail = submission.ContactPersonEmail,
                    FirstName = submission.FirstName,
                    Id = submission.Id,
                    IsoCode = submission.IsoCode,
                    JobDescription = submission.JobDescription,
                    LastName = submission.LastName,
                    MiddleInitial = submission.MiddleInitial,
                    SubmissionDate = submission.SubmissionDate,
                    SubmissionStatus = GetSubmissionStatus(submission.AdminResponse),
                    SectionId = submission.Student.SectionId,
                    ProgramId = submission.Student.ProgramId,
                    Comments = submission.AdminResponse.Comments
                });

            return mappedList;
        }

        private List<SubmissionDto.SubmissionStatus> GetSubmissionStatuses(IEnumerable<Submission> payload)
        {
            var mappedList = new List<SubmissionDto.SubmissionStatus>();
            var companyList = _companyRepository.GetAll().ToList();
            var adminResponseList = _adminResponse.GetAll().ToList();
            var companyMappedList =
                _mapper.Map<List<Company>, List<CompanyDto.ReadCompany>>(companyList);
            var mappedAdminResp =
                _mapper.Map<List<AdminResponse>, List<AdminResponseDto.ReadResponse>>(adminResponseList);
            foreach (var submission in payload)
                mappedList.Add(new SubmissionDto.SubmissionStatus
                {
                    CompanyName = companyMappedList.First(s => s.Id == submission.CompanyId).Name,
                    CompanyAddressTwo = companyMappedList.First(s => s.Id == submission.CompanyId).AddressTwo,
                    CompanyAddressOne = companyMappedList.First(s => s.Id == submission.CompanyId).AddressOne,
                    CompanyAddressThree = companyMappedList.First(s => s.Id == submission.CompanyId).AddressThree,
                    AdminResponseId = mappedAdminResp.First(s => s.SubmissionId == submission.Id).Id,
                    ContactPersonEmail = submission.ContactPersonEmail,
                    FirstName = submission.FirstName,
                    Id = submission.Id,
                    IsoCode = submission.IsoCode,
                    JobDescription = submission.JobDescription,
                    LastName = submission.LastName,
                    MiddleInitial = submission.MiddleInitial,
                    SubmissionDate = submission.SubmissionDate,
                    AcceptanceLetterFileName = submission.AcceptanceLetterFileName,
                    CompanyProfileFileName = submission.CompanyProfileFileName,
                    ContactPersonPosition = submission.ContactPersonPosition,
                    ContactPersonFirstName = submission.ContactPersonFirstName,
                    ContactPersonLastName = submission.ContactPersonLastName,
                    ContactPersonTitle = submission.ContactPersonTitle,
                    TrackId = submission.TrackId,
                    StudentEmail = submission.Student.Account.Email,
                    StudentNumber = submission.StudentNumber,
                    StudentTitle = submission.StudentTitle
                });

            return mappedList;
        }
    }
}