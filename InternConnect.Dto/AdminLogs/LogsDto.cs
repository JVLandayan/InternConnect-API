using System;

namespace InternConnect.Dto.AdminLogs
{
    public class LogsDto
    {
        public class ReadLogs
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string SubmissionId { get; set; }
            public DateTime DateStamped { get; set; }
            public int AdminId { get; set; }
            public Context.Models.Admin Admin { get; set; }
        }

        public class AddLogs
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string SubmissionId { get; set; }
            public DateTime DateStamped { get; set; }
            public int AdminId { get; set; }
            public Context.Models.Admin Admin { get; set; }
        }
    }
}