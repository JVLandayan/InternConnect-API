using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IOpportunityRepository : IBaseRepository<Opportunity>
    {
        public IEnumerable<Opportunity>  GetAllOpportunitiesAndCompanies();
    }
}