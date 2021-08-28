using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;

namespace InternConnect.Data.Repositories
{
    public class OpportunityRepository : BaseRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(InternConnectContext context) : base(context)
        {
        }
    }
}