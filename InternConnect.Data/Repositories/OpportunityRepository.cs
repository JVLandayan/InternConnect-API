using System.Collections.Generic;
using System.Linq;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class OpportunityRepository : BaseRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(InternConnectContext context) : base(context)
        {
        }

        public IEnumerable<Opportunity> GetAllOpportunitiesAndCompanies()
        {
            return Context.Set<Opportunity>().Include(o=>o.Company).ToList();
        }
    }
}