using System;
using System.Collections.Generic;
using InternConnect.Dto.AdminResponse;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminResponseController : ControllerBase
    {
        private readonly IAdminResponseService _adminResponseService;

        public AdminResponseController(IAdminResponseService adminResponse)
        {
            _adminResponseService = adminResponse;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdminResponseDto.ReadResponse>> GetAllEntries()
        {
            return Ok(_adminResponseService.GetAllEntries());
        }

        [HttpGet("admin/{stepNum}")]
        public ActionResult<IEnumerable<AdminResponseDto.ReadResponse>> GetAllEntriesByStep(int stepNum)
        {
            try
            {
                return Ok(_adminResponseService.GetAllEntriesByStep(stepNum));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }


        [HttpPut("coordinator/{adminId}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByCoordinator(
            AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload, int adminId)
        {
            _adminResponseService.UpdateAcceptanceByCoordinator(payload, adminId);
            return NoContent();
        }

        [HttpPut("coordinator/email")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateEmailSent(
            AdminResponseDto.UpdateEmailSentResponse payload)
        {
            _adminResponseService.UpdateEmailSent(payload);

            return NoContent();
        }

        [HttpPut("coordinator/company")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateCompanyAgrees(
            AdminResponseDto.UpdateCompanyAgreesResponse payload)
        {
            _adminResponseService.UpdateCompanyAgrees(payload);


            return NoContent();
        }

        [HttpPut("chair")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByChair(
            AdminResponseDto.UpdateChairResponse payload)
        {
            _adminResponseService.UpdateAcceptanceByChair(payload);


            return NoContent();
        }

        [HttpPut("dean/{adminId}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByDean(
            AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            _adminResponseService.UpdateAcceptanceByDean(payload, adminId);


            return NoContent();
        }


        //public void UpdateAcceptanceByCoordinator(AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload);
        //public void UpdateEmailSent(AdminResponseDto.UpdateEmailSentResponse payload);
        //public void UpdateCompanyAgrees(AdminResponseDto.UpdateCompanyAgreesResponse payload);
        //public void UpdateAcceptanceByeByChair(AdminResponseDto.UpdateChairResponse payload);
        //public void UpdateAcceptanceByByDean(AdminResponseDto.UpdateDeanResponse payload);

        //public IEnumerable<AdminResponseDto.ReadResponse> GetAllEntriesByStep(int stepNumber);


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