using System;

namespace InternConnect.Context.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string IgaarpEmail { get; set; }
    }
}