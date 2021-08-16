using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;

namespace InternConnect.Dto.Program
{
    public class ProgramDto
    {

        public class ReadProgram
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public int? IsoCode { get; set; }

            public List<Track> Tracks { get; set; }

        }

        public class UpdateProgram
        {
            public string Name { get; set; }

        }

        public class AddProgram
        {
            public string Name { get; set; }
        }




    }
}
