using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class ProgramRepository : BaseRepository<Program>, IProgramRepository
    {
        public ProgramRepository(InternConnectContext context) : base(context)
        {
        }

        public IEnumerable<Program> GetAllProgramAndTracks()
        {
            return Context.Set<Program>().Include(p => p.Tracks).ToList();
        }

        public Program GetProgramAndTracks(int id)
        {
            return Context.Set<Program>().Include(p => p.Tracks).First(p => p.Id == id);
        }
    }
}