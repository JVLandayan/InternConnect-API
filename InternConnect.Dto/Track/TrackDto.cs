using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Dto.Program;

namespace InternConnect.Dto.Track
{
    public class TrackDto
    {
        public class ReadTrack
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProgramId { get; set; }
        }

        public class AddTrack
        {
            public string Name { get; set; }
            public int ProgramId { get; set; }
        }

        public class UpdateTrack
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProgramId { get; set; }
        }


    }
}
