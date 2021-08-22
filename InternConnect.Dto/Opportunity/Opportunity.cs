using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Opportunity
{
    public class OpportunityDto
    {

        public class ReadOpportunity
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Position { get; set; }
            public string Introduction { get; set; }
            public int CompanyId { get; set; }

        }
        public class UpdateOpportunity
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Position { get; set; }
            public string Introduction { get; set; }
            public int CompanyId { get; set; }

        }
        public class AddOpportunity
        {
            public string Title { get; set; }
            public string Position { get; set; }
            public string Introduction { get; set; }
            public int CompanyId { get; set; }

        }




    }
}
