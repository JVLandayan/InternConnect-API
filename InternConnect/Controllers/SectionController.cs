using System;
using System.Collections.Generic;
using InternConnect.Dto.Section;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService section)
        {
            _sectionService = section;
        }


        //GET /admin
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<SectionDto.ReadSection>> GetAllSections()
        {
            return Ok(_sectionService.GetAll());
        }

        //GET /admin/id
        [Authorize]
        [HttpGet("{id}", Name = "GetSection")]
        public ActionResult<IEnumerable<SectionDto.ReadSection>> GetSection(int id)
        {
            try
            {
                return Ok(_sectionService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Section doesn't exist");
            }
        }

        [Authorize(Roles = "Chair")]
        [HttpPut]
        public ActionResult<SectionDto.ReadSection> UpdateSection(SectionDto.UpdateSection payload)
        {
            _sectionService.UpdateSection(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean,Chair")]
        [HttpPost]
        public ActionResult<SectionDto.ReadSection> AddSection(SectionDto.AddSection payload)
        {
            var sectionData = _sectionService.AddSection(payload);
            return CreatedAtRoute(nameof(GetSection), new {sectionData.Id}, sectionData);
        }

        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteSection(int id)
        {
            _sectionService.DeleteSection(id);
            return NoContent();
        }
    }
}