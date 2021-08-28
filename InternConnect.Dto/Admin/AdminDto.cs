using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Dto.Admin
{
    public class AdminDto
    {
        public class UpdateAdmin
        {
            public string StampFileName { get; set; }
        }

        public class ReadAdmin
        {
            public int Id { get; set; }
            public string StampFileName { get; set; }
            public int AuthId { get; set; }
            public int? ProgramId { get; set; }
            public int? SectionId { get; set; }
            public int AccountId { get; set; }
            public List<Logs> Logs { get; set; }
            public List<Context.Models.Event> Events { get; set; }
        }
    }
}