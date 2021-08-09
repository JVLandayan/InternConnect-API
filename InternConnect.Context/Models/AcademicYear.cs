using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string IgaarpEmail { get; set; }
        public int PdfStateId { get; set; }
        public PdfState PdfState { get; set; }

        public Submission Submission { get; set; }
    }
}
