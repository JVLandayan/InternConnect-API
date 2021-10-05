using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Admin
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

        //[JsonIgnore]
        //[IgnoreDataMember]
        public Account Account { get; set; }

        public List<IsoCode> IsoCodes { get; set; }

        public List<Logs> Logs { get; set; }
        public List<Event> Events { get; set; }
    }
}