using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminLogs;
using InternConnect.Dto.Submission;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace InternConnect.Service.Main
{
    public interface ILogsService
    {
        public IEnumerable<LogsDto.ReadLogs> GetLogsByAdminEmail(string email);
        public IEnumerable<LogsDto.ReadLogs> GetAllLogs();
        public IEnumerable<LogsDto.ReadLogs> GetLogsBySubmissionId(int submissionId);

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

        public IEnumerable<LogsDto.ReadLogs> GetAllLogs()
        {
            var logsList = _logsRepository.GetAll();
            var mappedList = new List<LogsDto.ReadLogs>();

            foreach (var logs in logsList)
            {
                mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(logs));  
            }
            return mappedList;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogsByAdminEmail(string email)
        {
            var logsList = _logsRepository.GetAll().Where(l => l.ActorEmail == email);
            var mappedList = new List<LogsDto.ReadLogs>();

            foreach (var logs in logsList)
            {
                mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(logs));
            }
            return mappedList;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogsBySubmissionId(int submissionId)
        {
            var logsList = _logsRepository.GetAll().Where(l=>l.SubmissionId == submissionId);
            var mappedList = new List<LogsDto.ReadLogs>();

            foreach (var logs in logsList)
            {
                mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(logs));
            }
            return mappedList;
        }
    }
}