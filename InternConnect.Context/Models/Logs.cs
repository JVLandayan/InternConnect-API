using System;

namespace InternConnect.Context.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime DateStamped { get; set; }
        public string ActorEmail { get; set; }
        public string ActorType { get; set; }
        public string Action { get; set; }
        public Submission Submission { get; set; }
        public int SubmissionId { get; set; }
    }
}