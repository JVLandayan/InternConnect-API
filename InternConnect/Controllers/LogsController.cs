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


        [Authorize(Roles = "Dean,Coordinator")]
        [HttpGet("adminId")]
        public ActionResult<IEnumerable<LogsDto.ReadLogs>> GetLogs(int adminId)
        {
            return Ok(_logsService.GetLogs(adminId));
        }
    }
}