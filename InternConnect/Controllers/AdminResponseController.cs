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

        [Authorize(Roles = "Coordinator")]
        [HttpPut("coordinator/{adminId}/{isoCode}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByCoordinator(
            AdminResponseDto.UpdateAcceptanceOfCoordinatorResponse payload, int adminId, int isoCode)
        {
            _adminResponseService.UpdateAcceptanceByCoordinator(payload, adminId, isoCode);
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

        //[Authorize(Roles = "Dean")]
        [HttpPut("dean/{adminId}")]
        public ActionResult<AdminResponseDto.ReadResponse> UpdateAcceptanceByDean(
            AdminResponseDto.UpdateDeanResponse payload, int adminId)
        {
            _adminResponseService.UpdateAcceptanceByDean(payload, adminId);
            return NoContent();
        }
    }
}