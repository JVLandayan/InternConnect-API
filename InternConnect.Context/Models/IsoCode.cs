using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class IsoCode
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
        public bool Used { get; set; }
        public int? SubmissionId { get; set; }
        public Admin Admin { get; set; }
        public int AdminId { get; set; }
        
    }
}
