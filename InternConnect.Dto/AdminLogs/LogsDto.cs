using System;

namespace InternConnect.Dto.AdminLogs
{
    public class LogsDto
    {
        public class ReadLogs
        {
            public int Id { get; set; }
            public DateTime DateStamped { get; set; }
            public string ActorEmail { get; set; }
            public string ActorType { get; set; }
            public string Action { get; set; }
        }
    }
}