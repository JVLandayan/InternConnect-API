using System;
using System.ComponentModel.DataAnnotations;

namespace InternConnect.Dto.Event
{
    public class EventDto
    {
        public class ReadEvent
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime EndDate { get; set; }
            public int AdminId { get; set; }
        }

        public class AddEvent
        {
            [Required] public string Name { get; set; }

            [Required] public string EndDate { get; set; }

            [Required] public int AdminId { get; set; }
        }

        public class UpdateEvent
        {
            [Required] public int Id { get; set; }

            [Required] public string Name { get; set; }

            [Required] public DateTime EndDate { get; set; }
        }
    }
}