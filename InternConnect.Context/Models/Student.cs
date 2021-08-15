using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Student
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }


        public int SectionId { get; set; }
        public Section Section { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public List<Submission> Submissions { get; set; }

        public int ProgramId { get; set; }
        public Program Program { get; set; }



    }
}
