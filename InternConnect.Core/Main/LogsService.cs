using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminLogs;
using InternConnect.Dto.Submission;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace InternConnect.Service.Main
{
    public interface ILogsService
    {
        public IEnumerable<LogsDto.ReadLogs> GetLogsByAdminId(int adminId);
        public IEnumerable<LogsDto.ReadLogs> GetAllLogs();
        public IEnumerable<LogsDto.ReadLogs> GetLogsBySubmissionId(int submissionId);

    }

    public class LogsService : ILogsService
    {
        private readonly ILogsRepository _logsRepository;
        private readonly IMapper _mapper;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IAdminRepository _adminRepository;

        public LogsService(IMapper mapper, ILogsRepository logsRepository, ISubmissionRepository submissionRepository, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _logsRepository = logsRepository;
            _submissionRepository = submissionRepository;
            _adminRepository = adminRepository;
        }

        public IEnumerable<LogsDto.ReadLogs> GetAllLogs()
        {
            var logsList = _logsRepository.GetAll().ToList();
            List<LogsDto.ReadLogs> mappedList = _mapper.Map<List<Logs>, List<LogsDto.ReadLogs>>(logsList);
            return mappedList;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogsByAdminId(int adminId)
        {
            var logsList = _logsRepository.GetAll().Where(l => l.ActorEmail == _adminRepository.GetAdminWithEmail(adminId).Account.Email).ToList();
            List<LogsDto.ReadLogs> mappedList = _mapper.Map<List<Logs>, List<LogsDto.ReadLogs>>(logsList);
            return mappedList;
        }

        public IEnumerable<LogsDto.ReadLogs> GetLogsBySubmissionId(int submissionId)
        {
            var logsList = _logsRepository.GetAll().Where(l=>l.SubmissionId == submissionId).ToList();
            

            List<LogsDto.ReadLogs> mappedList = _mapper.Map<List<Logs>, List<LogsDto.ReadLogs>>(logsList);
            
            return mappedList;
        }
    }
}