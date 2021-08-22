using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternConnect.Context.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternConnect.Dto.Event
{
    public class EventDto
    {
        public class ReadEvent
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int AdminId { get; set; }
        }
        public class AddEvent
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }
            [Required]
            public int AdminId { get; set; }
        }
        public class UpdateEvent
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }

        }


    }
}
