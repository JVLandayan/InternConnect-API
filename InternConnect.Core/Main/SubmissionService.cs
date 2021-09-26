using System;
using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminResponse;
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
    }

    public class SubmissionService : ISubmissionService
    {
        private readonly IAdminResponseRepository _adminResponseRepository;
        private readonly InternConnectContext _context;
        private readonly IMailerService _mailerService;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IProgramService _programService;
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionService(ISubmissionRepository submission, IMapper mapper,
            InternConnectContext context, IAdminResponseRepository adminResponse, IMailerService mailerService, IProgramService programService,
            IStudentService studentService)
        {
            _mapper = mapper;
            _context = context;
            _submissionRepository = submission;
            _adminResponseRepository = adminResponse;
            _mailerService = mailerService;
            _studentService = studentService;
            _programService = programService;
        }

        public SubmissionDto.ReadSubmission AddSubmission(SubmissionDto.AddSubmission payload, int sectionId, int programId)
        {
            var submissionData = _mapper.Map<Submission>(payload);
            if (_programService.GetById(programId).NumberOfHours == null)
            {
                return new SubmissionDto.ReadSubmission();
            }
            submissionData.IsoCode = (int)_programService.GetById(programId).NumberOfHours;
            var adminResponse = new AdminResponse();
            adminResponse.Comments = "";
            submissionData.AdminResponse = adminResponse;
            submissionData.SubmissionDate = DateTime.Now;
            _submissionRepository.Add(submissionData);
            _context.SaveChanges();
            _mailerService.NotifyCoordinator(sectionId);
            return _mapper.Map<SubmissionDto.ReadSubmission>(submissionData);
        }

        public IEnumerable<SubmissionDto.ReadSubmission> GetAllSubmissions()
        {
            var submissionList = _submissionRepository.GetAllRelatedData();
            var mappedList = new List<SubmissionDto.ReadSubmission>();
            foreach (var submission in submissionList)
                mappedList.Add(_mapper.Map<SubmissionDto.ReadSubmission>(submission));

            return mappedList;
        }

        public SubmissionDto.ReadSubmission GetSubmission(int id)
        {
            return _mapper.Map<SubmissionDto.ReadSubmission>(_submissionRepository.Get(id));
        }

        public void UpdateSubmission(SubmissionDto.UpdateSubmission payload)
        {
            payload.AdminResponse = new AdminResponseDto.AddResponse
            {
                AcceptedByChair = null,
                AcceptedByDean = null,
                EmailSentByCoordinator = null,
                AcceptedByCoordinator = null,
                Comments = null,
                CompanyAgrees = null
            };
            var submissionData = _submissionRepository.Get(payload.Id);
            _mapper.Map(payload, submissionData);
            _context.SaveChanges();
        }
    }
}