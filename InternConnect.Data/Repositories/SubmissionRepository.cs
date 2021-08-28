using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(InternConnectContext context) : base(context)
        {
        }
    }
}