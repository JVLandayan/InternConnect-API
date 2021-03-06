using System.Collections.Generic;
using System.Linq;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(InternConnectContext context) : base(context)
        {
        }

        public Admin GetAdminWithEmail(int id)
        {
            return Context.Set<Admin>().Include(a => a.Account).SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Admin> GetAllAdminsWithRelatedData()
        {
            return Context.Set<Admin>().Include(a => a.Section).Include(a => a.Program).Include(a => a.Account)
                .ToList();
        }
    }
}