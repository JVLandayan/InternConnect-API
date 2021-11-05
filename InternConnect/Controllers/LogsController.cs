using System.Collections.Generic;
using InternConnect.Dto.AdminLogs;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _logsService;

        public LogsController(ILogsService logs)
        {
            _logsService = logs;
        }

        [Authorize(Roles = "Dean,Chair,Coordinator,Tech Coordinator")]
        [HttpGet]
        public ActionResult<IEnumerable<LogsDto.ReadLogs>> GetAllLogs(int adminId)
        {
            return Ok(_logsService.GetAllLogs());
        }

        [Authorize(Roles = "Dean,Chair,Coordinator,Tech Coordinator")]
        [HttpGet("submission/{submissionId}")]
        public ActionResult<IEnumerable<LogsDto.ReadLogs>> GetLogsBySubmissionId(int submissionId)
        {
            return Ok(_logsService.GetLogsBySubmissionId(submissionId));
        }

        [Authorize(Roles = "Dean,Chair,Coordinator,Tech Coordinator")]
        [HttpGet("admin/{adminId}")]
        public ActionResult<IEnumerable<LogsDto.ReadLogs>> GetLogsByAdminEmail(int adminId)
        {
            return Ok(_logsService.GetLogsByAdminId(adminId));
        }
    }
}