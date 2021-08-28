using System;

namespace InternConnect.Context.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}