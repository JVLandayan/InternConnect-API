using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string HeaderFileName { get; set; }
        public string LogoFileName { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
        public List<Opportunity> Opportunities { get; set; }

    }
}
