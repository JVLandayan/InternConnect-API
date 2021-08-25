using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AcademicYear;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.Submission;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface ISubmissionService
    {
        public void AddSubmission(SubmissionDto.AddSubmission payload);
        public void UpdateSubmission(SubmissionDto.UpdateSubmission payload);
        public SubmissionDto.ReadSubmission GetSubmission(int id);
        public IEnumerable<SubmissionDto.ReadSubmission> GetAllSubmissions();

    }
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IAdminResponseRepository _adminResponseRepository;
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;

        public SubmissionService(ISubmissionRepository submission, IMapper mapper, 
            InternConnectContext context, IAdminResponseRepository adminResponse )
        {
            _mapper = mapper;
            _context = context;
            _submissionRepository = submission;
            _adminResponseRepository = adminResponse;
        }
        public void AddSubmission(SubmissionDto.AddSubmission payload)
        {
            var submissionData = _mapper.Map<Submission>(payload);
            var adminResponse = new AdminResponse();
            //{
            //    AcceptedByChair = null,
            //    AcceptedByDean = null,
            //    AcceptedByCoordinator = null,
            //    Comments = "",
            //    CompanyAgrees = null,
            //    EmailSentByCoordinator = null,
            //};
            adminResponse.Comments = "";
            submissionData.AdminResponse = adminResponse;


            _submissionRepository.Add(submissionData);
            _context.SaveChanges();
        }

        public IEnumerable<SubmissionDto.ReadSubmission> GetAllSubmissions()
        {
            var submissionList = _submissionRepository.GetAll();
            var mappedList = new List<SubmissionDto.ReadSubmission>();
            foreach (var submission in submissionList)
            {
               mappedList.Add(_mapper.Map<SubmissionDto.ReadSubmission>(submission)); 
            }

            return mappedList;
        }

        public SubmissionDto.ReadSubmission GetSubmission(int id)
        {
            return _mapper.Map<SubmissionDto.ReadSubmission>(_submissionRepository.Get(id));
        }

        public void UpdateSubmission(SubmissionDto.UpdateSubmission payload)
        {

            payload.AdminResponse = new AdminResponseDto.AddResponse()
            {
                AcceptedByChair = null,
                AcceptedByDean = null,
                EmailSentByCoordinator = null,
                AcceptedByCoordinator = null,
                Comments = null,
                CompanyAgrees = null,
            };
            var submissionData = _submissionRepository.Get(payload.Id);
            _mapper.Map(payload, submissionData);
            _context.SaveChanges();
        }
    }
}
