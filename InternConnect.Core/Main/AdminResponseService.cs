using System;
using System.Collections.Generic;
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
            int adminId);

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
        private readonly ILogsRepository _logsRepository;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;

        public AdminResponseService(InternConnectContext context, IMapper mapper,
            IAdminResponseRepository adminResponse, ILogsRepository logsRepository, IMailerService mailerService)
        {
            _context = context;
            _mapper = mapper;
            _adminResponseRepository = adminResponse;
            _logsRepository = logsRepository;
            _mailerService = mailerService;
        }

        public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload,
            int adminId)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            if (payload.AcceptedByCoordinator)
            {
                _logsRepository.Add(new Logs()
                    { DateStamped = DateTime.Now, AdminId = adminId, SubmissionId = responseData.SubmissionId });
            }
            _context.SaveChanges();
            _mailerService.NotifyChair(responseData.SubmissionId, adminId, payload.AcceptedByCoordinator);
        }

        public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
            _mailerService.NotifyStudentCompanyApproves(responseData.SubmissionId, payload.CompanyAgrees);
        }

        public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
            _mailerService.NotifyStudentEmailSent(responseData.SubmissionId, payload.EmailSentByCoordinator);
        }


        public void UpdateAcceptanceByDean(AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            if (payload.AcceptedByDean)
            {
                _logsRepository.Add(new Logs
                    { DateStamped = DateTime.Now, AdminId = adminId, SubmissionId = responseData.SubmissionId });
            }
            _context.SaveChanges();
            _mailerService.NotifyCoordAndIgaarp(responseData.SubmissionId, payload.AcceptedByDean);

        }


        public void UpdateAcceptanceByChair(AdminResponseDto.UpdateChairResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
            _mailerService.NotifyDean(responseData.SubmissionId, payload.AcceptedByChair);
        }

        //public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntriesByStep(int stepNumber)
        //{
        //    var responseList = _adminResponseRepository.GetAll().Where(ar => ar.AcceptedByCoordinator == null);

        //    if (stepNumber == 2)
        //        responseList = _adminResponseRepository.GetAll()
        //            .Where(ar => ar.AcceptedByCoordinator == true && ar.AcceptedByChair == null);
        //    if (stepNumber == 3)
        //        responseList = _adminResponseRepository.GetAll()
        //            .Where(ar => ar.AcceptedByChair == true && ar.AcceptedByDean == null);
        //    if (stepNumber == 4)
        //        responseList = _adminResponseRepository.GetAll()
        //            .Where(ar => ar.AcceptedByDean == true && ar.EmailSentByCoordinator == null);
        //    if (stepNumber == 5)
        //        responseList = _adminResponseRepository.GetAll()
        //            .Where(ar => ar.EmailSentByCoordinator == true && ar.CompanyAgrees == null);

        //    var mappedList = new List<AdminResponseDto.ReadResponse>();
        //    foreach (var response in responseList) mappedList.Add(_mapper.Map<AdminResponseDto.ReadResponse>(response));

        //    return mappedList;
        //}

        //public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntries()
        //{
        //    var responseList = _adminResponseRepository.GetAll();
        //    var mappedList = new List<AdminResponseDto.ReadResponse>();
        //    foreach (var response in responseList) mappedList.Add(_mapper.Map<AdminResponseDto.ReadResponse>(response));

        //    return mappedList;
        //}
    }
}