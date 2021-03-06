using System;
using System.Collections.Generic;
using InternConnect.Dto.AcademicYear;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYear)
        {
            _academicYearService = academicYear;
        }

        //GET /admin

        //GET /admin/id
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<AcademicYearDto.ReadAcademicYear>> GetAcademicYear()
        {
            if (_academicYearService.GetAcademicYear() != null)
                return Ok(_academicYearService.GetAcademicYear());

            return BadRequest("Academic Year isn't set");
        }

        [Authorize(Roles = "Dean")]
        [HttpPut]
        public ActionResult<AcademicYearDto.ReadAcademicYear> UpdateAcademicYear(
            AcademicYearDto.UpdateAcademicYear payload)
        {
            try
            {
                _academicYearService.UpdateAcademicYear(payload);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Dean")]
        [HttpPost]
        public ActionResult<AcademicYearDto.ReadAcademicYear> AddAcademicYear(
            AcademicYearDto.AddAcademicYear payload)
        {
            var ayData = _academicYearService.AddAcademicYear(payload);
            if (ayData == null) return NoContent();
            return BadRequest("Academic year existing");
        }
    }
}