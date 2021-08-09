using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Context.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgramId { get; set; }
        public Program Programs { get; set; }
        

    }
}
