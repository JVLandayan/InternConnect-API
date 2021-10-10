﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
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
        public IEnumerable<SubmissionDto.ReadSubmission> GetAllSubmissions();
        public IEnumerable<SubmissionDto.ReadSubmission> GetSubmissionsByStep(int stepNumber);
    }

    public class SubmissionService : ISubmissionService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly InternConnectContext _context;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;
        private readonly IProgramService _programService;
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionService(ISubmissionRepository submission, IMapper mapper,
            InternConnectContext context, IMailerService mailerService,
            IProgramService programService,
             ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _context = context;
            _submissionRepository = submission;

            _mailerService = mailerService;

            _programService = programService;
            _companyRepository = companyRepository;
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
            submissionData.SubmissionDate = DateTime.Now;
            _submissionRepository.Add(submissionData);
            _context.SaveChanges();
            _mailerService.NotifyCoordinator(sectionId);
            return _mapper.Map<SubmissionDto.ReadSubmission>(submissionData);
        }

        public IEnumerable<SubmissionDto.ReadSubmission> GetAllSubmissions()
        {
            var submissionList = _submissionRepository.GetAllRelatedData().ToList();
            var mappedList = new List<SubmissionDto.ReadSubmission>();
            foreach (var submission in submissionList)
                mappedList.Add(_mapper.Map<SubmissionDto.ReadSubmission>(submission));

            foreach (var submission in mappedList)
                submission.Company = _mapper.Map<CompanyDto.ReadCompany>(_companyRepository.Get(submission.CompanyId));
            return mappedList;
        }

        public SubmissionDto.ReadSubmission GetSubmission(int id)
        {
            return _mapper.Map<SubmissionDto.ReadSubmission>(_submissionRepository.GetAllRelatedData().ToList()
                .Last(s => s.Id == id));
        }

        public IEnumerable<SubmissionDto.ReadSubmission> GetSubmissionsByStep(int stepNumber)
        {
            var submissionList = new List<Submission>();
            var mappedList = new List<SubmissionDto.ReadSubmission>();

            if (stepNumber == 1)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s => s.AdminResponse.AcceptedByCoordinator == null &&
                                  s.AdminResponse.AcceptedByChair == null);

            else if (stepNumber == 2)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s =>
                        s.AdminResponse.AcceptedByCoordinator == true && s.AdminResponse.AcceptedByChair == null);
            else if (stepNumber == 3)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s => s.AdminResponse.AcceptedByChair == true && s.AdminResponse.AcceptedByDean == null);
            else if (stepNumber == 4)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s =>
                        s.AdminResponse.AcceptedByDean == true && s.AdminResponse.EmailSentByCoordinator == null);
            else if (stepNumber == 5)
                submissionList = _submissionRepository.GetAllRelatedData().ToList()
                    .FindAll(s =>
                        s.AdminResponse.EmailSentByCoordinator == true && s.AdminResponse.CompanyAgrees == null);

            foreach (var submission in submissionList)
                mappedList.Add(_mapper.Map<SubmissionDto.ReadSubmission>(submission));

            foreach (var submission in mappedList)
                submission.Company = _mapper.Map<CompanyDto.ReadCompany>(_companyRepository.Get(submission.CompanyId));


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
        }
    }
}