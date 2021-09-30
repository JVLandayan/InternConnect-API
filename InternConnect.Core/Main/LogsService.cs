using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminLogs;
using InternConnect.Dto.Submission;

namespace InternConnect.Service.Main
{
    public interface ILogsService
    {
        public IEnumerable<LogsDto.ReadLogs> GetLogs(int adminId);
    }

    public class LogsService : ILogsService
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IMapper _mapper;
        private readonly ISubmissionRepository _submissionRepository;

        public LogsService(IMapper mapper, ILogsRepository logsRepository, ISubmissionRepository submissionRepository)
        {
            _mapper = mapper;
            _logsRepository = logsRepository;
            _submissionRepository = submissionRepository;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogs(int adminId)
        {
            var logsList = _logsRepository.GetAll().Where(log => log.AdminId == adminId).ToList();
            var mappedList = new List<LogsDto.ReadLogs>();
            foreach (var log in logsList) mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(log));
            var submissionList = _submissionRepository.GetAllRelatedData().ToList();
            foreach (var log in mappedList)
            foreach (var submission in submissionList.Where(s => s.Id == log.SubmissionId).ToList())
                log.Submission =
                    _mapper.Map<SubmissionDto.ReadSubmission>(submission);

            foreach (var log in mappedList)
                if (mappedList.Last(l => l.SubmissionId == log.SubmissionId) == log)
                    log.Status = LogStatus(log.Submission.AdminResponse.Id);
                else
                    log.Status = "Rejected";


            return mappedList;
        }

        private string LogStatus(int responseId)
        {
            var responseData = _submissionRepository.GetAllRelatedData().First(s => s.AdminResponse.Id == responseId)
                .AdminResponse;

            if (responseData.AcceptedByCoordinator == null && responseData.AcceptedByChair == null)
                return Status.StatusList.NEW_SUBMISSION.ToString();
            if (responseData.AcceptedByCoordinator == true && responseData.AcceptedByChair == null)
                return Status.StatusList.ACCEPTEDBYCOORDINATOR.ToString();
            if (responseData.AcceptedByChair == true && responseData.AcceptedByDean == null)
                return Status.StatusList.ACCEPTEDBYCHAIR.ToString();
            if (responseData.AcceptedByDean == true && responseData.EmailSentByCoordinator == null)
                return Status.StatusList.ACCEPTEDBYDEAN.ToString();
            if (responseData.EmailSentByCoordinator == true && responseData.CompanyAgrees == null)
                return Status.StatusList.EMAILSENTTOCOMPANY.ToString();
            if (responseData.CompanyAgrees == true) return Status.StatusList.COMPANYAGREES.ToString();

            return "Error";
        }
    }
}