using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(InternConnectContext context) : base(context)
        {
        }

        public IEnumerable<Submission> GetAllRelatedData()
        {
            return Context.Set<Submission>().Include(s => s.Student).Include(s=>s.AdminResponse);
        }
    }
}