using System;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class LogsRepository: BaseRepository<Logs>, ILogsRepository
    {
        public LogsRepository(DbContext context) : base(context)
        {

        }


    }
}
