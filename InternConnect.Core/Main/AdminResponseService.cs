using System;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminResponse;
using InternConnect.Service.ThirdParty;

namespace InternConnect.Service.Main
{
    public interface IAdminResponseService
    {
        public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload,
            int adminId, int isoCode);

        public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload);
        public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload);
        public void UpdateAcceptanceByChair(AdminResponseDto.UpdateChairResponse payload);
        public void UpdateAcceptanceByDean(AdminResponseDto.UpdateDeanResponse payload, int adminId);

        //public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntriesByStep(int stepNumber);
        //public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntries();
    }

    public class AdminResponseService : IAdminResponseService
    {
        private readonly IAdminResponseRepository _adminResponseRepository;
        private readonly InternConnectContext _context;
        private readonly IIsoCodeRepository _isoCodeRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogsRepository _logsRepository;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;

        public AdminResponseService(InternConnectContext context, IMapper mapper,
            IAdminResponseRepository adminResponse, ILogsRepository logsRepository, IMailerService mailerService,
            ISubmissionRepository submissionRepository, IIsoCodeRepository isoCodeRepository, IAdminRepository adminRepository, ICompanyRepository companyRepository)
        {
            _context = context;
            _mapper = mapper;
            _adminResponseRepository = adminResponse;
            _logsRepository = logsRepository;
            _mailerService = mailerService;
            _isoCodeRepository = isoCodeRepository;
            _adminRepository = adminRepository;
            _companyRepository = companyRepository;
        }

        public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload,
            int adminId, int isoCode)
        {
            var responseData = _adminResponseRepository.GetAdminResponseWithSubmission(payload.Id);
            var getAdminId = _adminRepository.GetAll().Where(a=>a.AuthId == 3)
                .First(a => a.SectionId == responseData.Submission.Student.SectionId).Id;
            var adminData = _adminRepository.GetAdminWithEmail(getAdminId);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs()
                    {
                        Action =
                            $"ENDORSEMENT REQUEST OF {responseData.Submission.Student.Account.Email} {(payload.AcceptedByCoordinator ? "ACCEPTED" : "REJECTED")} ",
                        DateStamped = GetDate(),
                        SubmissionId = responseData.SubmissionId,
                        ActorEmail = adminData.Account.Email,
                        ActorType = _context.Set<Authorization>().Find(adminData.AuthId).Name
                    }
                );

            responseData.Submission.IsoCode = isoCode;
            var isoCodeData = _isoCodeRepository.GetAll().First(a => a.Code == isoCode && a.AdminId == getAdminId);
            isoCodeData.Used = true;
            isoCodeData.SubmissionId = responseData.Submission.Id;
            _context.SaveChanges();
            try
            {
                _mailerService.NotifyChair(responseData.SubmissionId, getAdminId, payload.AcceptedByCoordinator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload)
        {
            var responseData = _adminResponseRepository.GetAdminResponseWithSubmission(payload.Id);
            var getAdminDatabySection = _adminRepository.GetAll().Where(a => a.AuthId == 3)
                .First(a => a.SectionId == responseData.Submission.Student.SectionId).Id;
            var adminData = _adminRepository.GetAdminWithEmail(getAdminDatabySection);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs()
                {
                    Action =
                        $"{_companyRepository.Get(responseData.Submission.CompanyId).Name.ToUpper()} {(payload.CompanyAgrees ? "ACCEPTS" : "REJECTS")} {responseData.Submission.Student.Account.Email}",
                    DateStamped = GetDate(),
                    SubmissionId = responseData.SubmissionId,
                    ActorEmail = adminData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(adminData.AuthId).Name
                }
            );
            _context.SaveChanges();
            try
            {
                _mailerService.NotifyStudentCompanyApproves(responseData.SubmissionId, payload.CompanyAgrees);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload)
        {
            var responseData = _adminResponseRepository.GetAdminResponseWithSubmission(payload.Id);
            var getAdminDatabySection = _adminRepository.GetAll().Where(a => a.AuthId == 3)
                .First(a => a.SectionId == responseData.Submission.Student.SectionId).Id;
            var adminData = _adminRepository.GetAdminWithEmail(getAdminDatabySection);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs()
                {
                    Action =
                        $"ENDORSEMENT LETTER OF {responseData.Submission.Student.Account.Email} SENT TO {_companyRepository.Get(responseData.Submission.CompanyId).Name.ToUpper()}",
                    DateStamped = GetDate(),
                    SubmissionId = responseData.SubmissionId,
                    ActorEmail = adminData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(adminData.AuthId).Name
                }
            );
            _context.SaveChanges();

            try
            {
                _mailerService.NotifyStudentEmailSent(responseData.SubmissionId, payload.EmailSentByCoordinator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void UpdateAcceptanceByDean(AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            var responseData = _adminResponseRepository.GetAdminResponseWithSubmission(payload.Id);
            _mapper.Map(payload, responseData);
            var getAdminDatabySection = _adminRepository.GetAll().First(a => a.AuthId == 1).Id;
            var adminData = _adminRepository.GetAdminWithEmail(getAdminDatabySection);
            _logsRepository.Add(new Logs()
                {
                    Action =
                        $"ENDORSEMENT REQUEST OF {responseData.Submission.Student.Account.Email} {(payload.AcceptedByDean?"ACCEPTED":"REJECTED")}",
                    DateStamped = GetDate(),
                    SubmissionId = responseData.SubmissionId,
                    ActorEmail = adminData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(adminData.AuthId).Name
                }
            );
            _context.SaveChanges();
            _mailerService.NotifyCoordAndIgaarp(responseData.SubmissionId, payload.AcceptedByDean);
        }


        public void UpdateAcceptanceByChair(AdminResponseDto.UpdateChairResponse payload)
        {
            var responseData = _adminResponseRepository.GetAdminResponseWithSubmission(payload.Id);
            var getAdminDatabySection = _adminRepository.GetAll().Where(a => a.AuthId == 2).First(a=>a.ProgramId == responseData.Submission.Student.ProgramId).Id;
            var adminData = _adminRepository.GetAdminWithEmail(getAdminDatabySection);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs()
                {
                    Action =
                        $"ENDORSEMENT REQUEST OF {responseData.Submission.Student.Account.Email} {(payload.AcceptedByChair?"ACCEPTED":"REJECTED")}",
                    DateStamped = GetDate(),
                    SubmissionId = responseData.SubmissionId,
                    ActorEmail = adminData.Account.Email,
                    ActorType = _context.Set<Authorization>().Find(adminData.AuthId).Name
                }
            );
            _context.SaveChanges();
            try
            {
                _mailerService.NotifyDean(responseData.SubmissionId, payload.AcceptedByChair);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now,
                TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}