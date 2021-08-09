using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Track> Tracks { get; set; }
        

        public Student Student { get; set; }

        public Admin Admin { get; set; }

        public Event Event { get; set; }

    }
}
