using System;

namespace InternConnect.Context.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Introduction { get; set; }
        public DateTime DateAdded { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}