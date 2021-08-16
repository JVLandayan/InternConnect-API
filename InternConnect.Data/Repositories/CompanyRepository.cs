using System.Collections.Generic;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class CompanyRepository: BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {

        }

    }
}
