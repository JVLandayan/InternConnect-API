using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using InternConnect.Dto.Program;
using InternConnect.Dto.Section;

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
            public int ProgramId { get; set; }
        }


        public class UpdateStudent
        {
            public int Id { get; set; }
            public int SectionId { get; set; }
            public SectionDto.ReadSection Section { get; set; }
            public int ProgramId { get; set; }
            public ProgramDto.ReadProgram Program { get; set; }

        }


    }
}
