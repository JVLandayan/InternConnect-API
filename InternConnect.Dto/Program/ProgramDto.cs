using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternConnect.Dto.Track;

namespace InternConnect.Dto.Program
{
    public class ProgramDto
    {
        public class ReadProgram
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string IsoCodeProgramNumber { get; set; }
            public int? NumberOfHours { get; set; }
            public int IsActive { get; set; }
            public List<TrackDto.ReadTrack> Tracks { get; set; }
        }

        public class UpdateProgram
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string IsoCodeProgramNumber { get; set; }
            [Required]
            public int NumberOfHours { get; set; }
        }


        public class AddProgram
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string IsoCodeProgramNumber { get; set; }
            [Required]
            public int NumberOfHours { get; set; }
        }
    }
}