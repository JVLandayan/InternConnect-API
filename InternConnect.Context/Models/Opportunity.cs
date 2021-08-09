using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Introduction { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
