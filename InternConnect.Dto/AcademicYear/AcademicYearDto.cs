using System;
using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.AcademicYear
{
    public class AcademicYearDto
    {
        public class ReadAcademicYear
        {
            public int Id { get; set; }
            public string CollegeName { get; set; }
            public int StartYear { get; set; }
            public int EndYear { get; set; }
            public string IgaarpEmail { get; set; }
        }

        public class AddAcademicYear
        {
            [Required] public string CollegeName { get; set; }

            [Required] public int StartYear { get; set; }
            [Required] public int EndYear { get; set; }

            [Required] public string IgaarpEmail { get; set; }
        }

        public class UpdateAcademicYear
        {
            [Required] public int Id { get; set; }

            [Required] public string CollegeName { get; set; }

            [Required] public string IgaarpEmail { get; set; }
        }
    }
}