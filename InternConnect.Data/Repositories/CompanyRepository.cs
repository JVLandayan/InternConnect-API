using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(InternConnectContext context) : base(context)
        {
        }
    }
}