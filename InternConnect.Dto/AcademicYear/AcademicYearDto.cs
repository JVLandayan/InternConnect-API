using InternConnect.Context.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.AcademicYear
{
    public class AcademicYearDto
    {

        public class ReadAcademicYear
        {
            public int Id { get; set; }
            public string CollegeName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string IgaarpEmail { get; set; }
            public int PdfStateId { get; set; }
            public Context.Models.PdfState PdfState { get; set; }
        }

        public class AddAcademicYear
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string CollegeName { get; set; }
            [Required]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }
            [Required]
            public string IgaarpEmail { get; set; }
        }

        public class UpdateAcademicYear
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string CollegeName { get; set; }
            [Required]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }
            [Required]
            public string IgaarpEmail { get; set; }

        }
    }
}
