using System;
using System.Collections.Generic;
using InternConnect.Dto.PdfState;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfStateController : ControllerBase
    {
        private readonly IPdfStateService _pdfStateService;

        public PdfStateController(IPdfStateService pdfState)
        {
            _pdfStateService = pdfState;
        }

        //GET /admin

        //GET /admin/id
        [HttpGet]
        public ActionResult<IEnumerable<PdfStateDto.ReadPdfState>> GetPdfState()
        {
            try
            {
                return Ok(_pdfStateService.GetPdfState());
            }
            catch (Exception e)
            {
                return BadRequest("Pdf state isn't set");
            }
            
        }


        [HttpPut]
        public ActionResult<PdfStateDto.ReadPdfState> UpdatePdfState(PdfStateDto.UpdatePdfState payload)
        {
            _pdfStateService.UpdatePdfState(payload);
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