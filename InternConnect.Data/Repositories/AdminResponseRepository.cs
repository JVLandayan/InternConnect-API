using System.Linq;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class AdminResponseRepository : BaseRepository<AdminResponse>, IAdminResponseRepository
    {
        public AdminResponseRepository(InternConnectContext context) : base(context)
        {
        }

        public AdminResponse GetAdminResponseWithSubmission(int id)
        {
            return Context.Set<AdminResponse>().Include(a => a.Submission).ThenInclude(s => s.Student)
                .ThenInclude(student => student.Account).SingleOrDefault(a => a.Id == id);
        }
    }
}