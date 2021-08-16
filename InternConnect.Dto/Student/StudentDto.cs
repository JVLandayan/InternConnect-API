using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;

namespace InternConnect.Dto.Student
{
    public class StudentDto
    {
        public class ReadStudent
        {
            public int Id { get; set; }
            public DateTime DateAdded { get; set; }
            public string AddedBy { get; set; }
            public int SectionId { get; set; }
            public Context.Models.Section Section { get; set; }
            public int ProgramId { get; set; }
            public Context.Models.Program Program { get; set; }

        }

        public class UpdateStudent
        {
            public int Id { get; set; }
            public int SectionId { get; set; }
            public Context.Models.Section Section { get; set; }
            public int ProgramId { get; set; }
            public Context.Models.Program Program { get; set; }

        }


    }
}
