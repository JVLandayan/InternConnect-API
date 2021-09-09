using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IProgramRepository : IBaseRepository<Program>
    {
        public IEnumerable<Program> GetAllProgramAndTracks();
    }
}