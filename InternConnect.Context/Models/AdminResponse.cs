namespace InternConnect.Context.Models
{
    public class AdminResponse
    {
        public int Id { get; set; }
        public bool? AcceptedByCoordinator { get; set; }
        public bool? AcceptedByChair { get; set; }
        public bool? AcceptedByDean { get; set; }
        public bool? EmailSentByCoordinator { get; set; }
        public bool? CompanyAgrees { get; set; }
        public string Comments { get; set; }
        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}