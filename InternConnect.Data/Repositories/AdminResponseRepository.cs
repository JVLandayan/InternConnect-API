using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class AdminResponseRepository: BaseRepository<AdminResponse>, IAdminResponseRepository
    {
        public AdminResponseRepository(InternConnectContext context) : base(context)
        {

        }
    }
}
