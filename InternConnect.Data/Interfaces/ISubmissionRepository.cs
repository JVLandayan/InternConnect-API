using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface ISubmissionRepository : IBaseRepository<Submission>
    {
        public IEnumerable<Submission> GetAllRelatedData();
    }
}