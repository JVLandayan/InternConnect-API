using System;

namespace InternConnect.Context.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public DateTime DateStamped { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}