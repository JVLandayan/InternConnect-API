using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class ProgramRepository : BaseRepository<Program>, IProgramRepository
    {
        public ProgramRepository(InternConnectContext context) : base(context)
        {
        }
    }
}