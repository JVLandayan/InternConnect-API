using System;

namespace InternConnect.Context.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; } 
        public string IgaarpEmail { get; set; }
    }
}