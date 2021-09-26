using System;
using System.Collections.Generic;
using InternConnect.Dto.AdminResponse;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Coordinator,Dean,Chair")]
        //[HttpGet]
        //public ActionResult<IEnumerable<AdminResponseDto.ReadResponse>> GetAllEntries()
        //{
        //    return Ok(_adminResponseService.GetAllEntries());
        //}

        //[Authorize(Roles = "Coordinator,Dean,Chair")]
        //[HttpGet("admin/{stepNum}")]
        //public ActionResult<IEnumerable<AdminResponseDto.ReadResponse>> GetAllEntriesByStep(int stepNum)
        //{
        //    try
        //    {
        //        return Ok(_adminResponseService.GetAllEntriesByStep(stepNum));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        [Authorize(Roles = "Coordinator")]
        [HttpPut("coordinator/{adminId}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByCoordinator(
            AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload, int adminId)
        {
            _adminResponseService.UpdateAcceptanceByCoordinator(payload, adminId);
            return NoContent();
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPut("coordinator/email")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateEmailSent(
            AdminResponseDto.UpdateEmailSentResponse payload)
        {
            _adminResponseService.UpdateEmailSent(payload);

            return NoContent();
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPut("coordinator/company")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateCompanyAgrees(
            AdminResponseDto.UpdateCompanyAgreesResponse payload)
        {
            _adminResponseService.UpdateCompanyAgrees(payload);


            return NoContent();
        }

        [Authorize(Roles = "Chair")]
        [HttpPut("chair")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByChair(
            AdminResponseDto.UpdateChairResponse payload)
        {
            _adminResponseService.UpdateAcceptanceByChair(payload);


            return NoContent();
        }

        [Authorize(Roles = "Dean")]
        [HttpPut("dean/{adminId}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByDean(
            AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            _adminResponseService.UpdateAcceptanceByDean(payload, adminId);


            return NoContent();
        }


    }
}