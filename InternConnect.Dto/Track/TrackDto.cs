using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Track
{
    public class TrackDto
    {
        public class ReadTrack
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProgramId { get; set; }
            public Context.Models.Program Programs { get; set; }
        }

        public class AddTrack
        {
            public string Name { get; set; }
            public int ProgramId { get; set; }
            public Context.Models.Program Programs { get; set; }
        }

        public class UpdateTrack
        {
            public string Name { get; set; }
            public int ProgramId { get; set; }
            public Context.Models.Program Programs { get; set; }
        }


    }
}
