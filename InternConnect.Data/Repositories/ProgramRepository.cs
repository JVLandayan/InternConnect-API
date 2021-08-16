using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class ProgramRepository: BaseRepository<Program>, IProgramRepository
    {
        public ProgramRepository(DbContext context) : base(context)
        {

        }


    }
}
