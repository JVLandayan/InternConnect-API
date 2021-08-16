using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(DbContext context) : base(context)
        {

        }



    }
}
