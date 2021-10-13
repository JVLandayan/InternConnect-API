using System.ComponentModel.DataAnnotations;

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
            [Required] public string Name { get; set; }
            [Required] public int ProgramId { get; set; }
        }

        public class UpdateTrack
        {
            [Required] public int Id { get; set; }
            [Required] public string Name { get; set; }
            [Required] public int ProgramId { get; set; }
        }
    }
}