﻿using InternConnect.Service.Main.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data;
using InternConnect.Dto.AcademicYear;
using InternConnect.Dto.Account;
using InternConnect.Dto.Admin;
using InternConnect.Dto.Section;
using InternConnect.Dto.Student;
using InternConnect.Dto.Track;
using InternConnect.Dto.WebState;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AcademicYearDto.ReadAcademicYear>> GetAcademicYear(int id)
        {
            return Ok(_academicYearService.GetAcademicYear(id));
        }


        [HttpPut]
        public ActionResult<AcademicYearDto.ReadAcademicYear> UpdateAcademicYear(AcademicYearDto.UpdateAcademicYear payload)
        {
            _academicYearService.UpdateAcademicYear(payload);
            return NoContent();
        }



        //[Authorize]
        //[HttpPut("{id}")]

        //public ActionResult UpdateTeam(int id, TeamsUpdateDto teamsUpdateDto)
        //{
        //    var teamsModelFromRepo = _repository.GetTeamById(id);

        //    if (teamsModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    _mapper.Map(teamsUpdateDto, teamsModelFromRepo);
        //    _repository.UpdateTeam(teamsModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();

        //}
        //[Authorize]
        //[HttpPatch("{id}")]

        //public ActionResult PartialTeamsUpdate(int id, JsonPatchDocument<TeamsUpdateDto> patchDoc)
        //{
        //    var teamModelFromRepo = _repository.GetTeamById(id);
        //    if (teamModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    var teamToPatch = _mapper.Map<TeamsUpdateDto>(teamModelFromRepo);
        //    patchDoc.ApplyTo(teamToPatch, ModelState);
        //    if (!TryValidateModel(teamToPatch))
        //    {
        //        return ValidationProblem();
        //    }
        //    _mapper.Map(teamToPatch, teamModelFromRepo);
        //    _repository.UpdateTeam(teamModelFromRepo);
        //    _repository.SaveChanges();
        //    return NoContent();

    }
}
