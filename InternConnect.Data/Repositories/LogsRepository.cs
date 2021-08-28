using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class LogsRepository : BaseRepository<Logs>, ILogsRepository
    {
        public LogsRepository(InternConnectContext context) : base(context)
        {
        }
    }
}