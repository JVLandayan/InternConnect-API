using System.Collections.Generic;

namespace InternConnect.Context.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCodeProgramNumber { get; set; }
        public int? NumberOfHours { get; set; }
        public List<Track> Tracks { get; set; }

        public List<Student> Students { get; set; }

        public List<Admin> Admins { get; set; }
        public List<IsoCode> IsoCodes { get; set; }
    }
}