using System;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class SubmissionRepository: BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(DbContext context) : base(context)
        {

        }




    }
}
