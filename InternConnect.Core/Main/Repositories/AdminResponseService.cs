using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Policy;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminResponse;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface IAdminResponseService
    {
        public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload, int adminId);
        public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload);
        public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload);
        public void UpdateAcceptanceByChair(AdminResponseDto.UpdateChairResponse payload);
        public void UpdateAcceptanceByDean(AdminResponseDto.UpdateDeanResponse payload, int adminId);

        public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntriesByStep(int stepNumber);
        public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntries();
    }
    public class AdminResponseService : IAdminResponseService
    {
        private readonly InternConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IAdminResponseRepository _adminResponseRepository;
        private readonly ILogsRepository _logsRepository;

        public AdminResponseService(InternConnectContext context, IMapper mapper, IAdminResponseRepository adminResponse, ILogsRepository logsRepository)
        {
            _context = context;
            _mapper = mapper;
            _adminResponseRepository = adminResponse;
            _logsRepository = logsRepository;
        }

        public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload, int adminId)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs(){DateStamped = DateTime.Now, AdminId = adminId, SubmissionId = responseData.SubmissionId});
            _context.SaveChanges();

        }

        public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
        }

        public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
        }


        public void UpdateAcceptanceByDean(AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _logsRepository.Add(new Logs() { DateStamped = DateTime.Now, AdminId = adminId, SubmissionId = responseData.SubmissionId });
            _context.SaveChanges();
        }



        public void UpdateAcceptanceByChair(AdminResponseDto.UpdateChairResponse payload)
        {
            var responseData = _adminResponseRepository.Get(payload.Id);
            _mapper.Map(payload, responseData);
            _context.SaveChanges();
        }

        public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntriesByStep(int stepNumber)
        {
            var responseList = _adminResponseRepository.GetAll().Where(ar => ar.AcceptedByCoordinator == null);

            if (stepNumber == 2)
            {
                responseList = _adminResponseRepository.GetAll().Where(ar => ar.AcceptedByCoordinator == true && ar.AcceptedByChair == null);
            }
            if (stepNumber == 3)
            {
                responseList = _adminResponseRepository.GetAll().Where(ar => ar.AcceptedByChair == true && ar.AcceptedByDean == null);
            }
            if (stepNumber == 4)
            {
                responseList = _adminResponseRepository.GetAll().Where(ar => ar.AcceptedByDean == true && ar.EmailSentByCoordinator == null);
            }
            if (stepNumber == 5)
            {
                responseList = _adminResponseRepository.GetAll().Where(ar => ar.EmailSentByCoordinator == true && ar.CompanyAgrees == null);
            }

            var mappedList = new List<AdminResponseDto.ReadResponse>();
            foreach (var response in responseList)
            {
                mappedList.Add(_mapper.Map<AdminResponseDto.ReadResponse>(response));
            }

            return mappedList;

        }

        public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntries()
        {
            var responseList = _adminResponseRepository.GetAll();
            var mappedList =  new List<AdminResponseDto.ReadResponse>();
            foreach (var response in responseList)
            {
                mappedList.Add(_mapper.Map<AdminResponseDto.ReadResponse>(response));
            }

            return mappedList;
        }



    }


}
