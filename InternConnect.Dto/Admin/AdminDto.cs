using InternConnect.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Admin
{
    public class AdminDto
    {
        public class AddAdmin
        {
            
        }

        public class UpdateAdmin
        {

            public string StampFileName { get; set; }

        }

        public class ReadAdmin
        {
            public int Id { get; set; }
            public string StampFileName { get; set; }
            public int AuthId { get; set; }
            public Authorization Authorization { get; set; }
            public int? ProgramId { get; set; }
            public Program Program { get; set; }
            public int? SectionId { get; set; }
            public Section Section { get; set; }

            public int AccountId { get; set; }
            public Account Account { get; set; }

            public List<Logs> Logs { get; set; }
            public List<Event> Events { get; set; }

        }
    }
}
