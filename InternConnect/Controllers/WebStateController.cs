using System;
using System.Collections.Generic;
using InternConnect.Dto.WebState;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebStateController : ControllerBase
    {
        private readonly IWebStateService _webStateService;

        public WebStateController(IWebStateService webState)
        {
            _webStateService = webState;
        }

        //GET /admin

        //GET /admin/id
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<WebStateDto.ReadWebState>> GetWebState(int id)
        {
            try
            {
                return Ok(_webStateService.GetWebState(id));
            }
            catch (Exception e)
            {
                return BadRequest("State doesn't exist");
            }
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPut]
        public ActionResult<WebStateDto.ReadWebState> UpdateWebState(WebStateDto.UpdateWebState payload)
        {
            _webStateService.UpdateWebState(payload);
            return NoContent();
        }
        [Authorize(Roles = "Dean,Tech Coordinator,Chair")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,string type)
        {
            _webStateService.Remove(id, type);
            return NoContent();
        }
    }
}