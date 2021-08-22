using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using InternConnect.Dto.Track;

namespace InternConnect.Dto.Program
{
    public class ProgramDto
    {

        public class ReadProgram
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public int? IsoCode { get; set; }

            public List<TrackDto.ReadTrack> Tracks { get; set; }

        }

        public class UpdateProgram
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public class UpdateIsoCode
        {
            public int Id { get; set; }
            public int IsoCode { get; set; }

        }

        public class AddProgram
        {
            public string Name { get; set; }
        }




    }
}
