using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.AdminLogs;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{

    public interface ILogsService
    {
        public IEnumerable<LogsDto.ReadLogs> GetLogs(int adminId);
    }

    public class LogsService : ILogsService
    {
        private readonly IMapper _mapper;
        private readonly ILogsRepository _logsRepository;

        public LogsService(IMapper mapper, ILogsRepository logsRepository)
        {
            _mapper = mapper;
            _logsRepository = logsRepository;
        }
        public IEnumerable<LogsDto.ReadLogs> GetLogs(int adminId)
        {
            var logsList = _logsRepository.GetAll().Where(log => log.AdminId == adminId);
            var mappedList = new List<LogsDto.ReadLogs>();
            foreach (var log in logsList)
            {
                mappedList.Add(_mapper.Map<LogsDto.ReadLogs>(log));
            }
            return mappedList;
        }
    }
}
