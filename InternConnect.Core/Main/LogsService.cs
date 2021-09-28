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
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IMapper _mapper;

        public LogsService(IMapper mapper, ILogsRepository logsRepository, ISubmissionRepository submissionRepository)
        {
            _mapper = mapper;
            _logsRepository = logsRepository;
            _submissionRepository = submissionRepository;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogs(int adminId)
        {
            var logsList = _logsRepository.GetAll().Where(log => log.AdminId == adminId);
            var mappedList = new List<LogsDto.ReadLogs>();
            foreach (var log in logsList) mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(log));
            var submissionData = _submissionRepository.GetAllRelatedData().ToList();
            foreach (var log in mappedList)
            {
                log.Submission =
                    _mapper.Map<SubmissionDto.ReadSubmission>(submissionData.First(s => s.Id == log.SubmissionId));
            }
            return mappedList;
        }
    }
}