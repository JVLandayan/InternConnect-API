using System.Collections.Generic;
using InternConnect.Dto.AdminResponse;
using InternConnect.Dto.IsoCode;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsoCodeController : ControllerBase
    {
        private readonly IIsoCodeService _isoCodeService;

        public IsoCodeController(IIsoCodeService isoCodeService)
        {
            _isoCodeService = isoCodeService;
        }


        [Authorize(Roles = "Coordinator,Chair")] 
        [HttpGet("admin/{adminId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> GetAllByAdminId(int adminId)
        {
            var isoCodeList = _isoCodeService.GetAllByAdminId(adminId);
            if (isoCodeList != null)
            {
                return Ok(isoCodeList);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Chair")]
        [HttpGet("program/{programId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> GetAllByProgram(int programId)
        {
            var isoCodeList = _isoCodeService.GetAllByProgramId(programId);
            if (isoCodeList != null)
            {
                return Ok(isoCodeList);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Chair")]
        [HttpPost]
        public ActionResult<AdminResponseDto.ReadResponse> AddIsoCodes(IList<IsoCodeDto.AddIsoCode> payload)
        {
            _isoCodeService.BulkAdd(payload);
            return NoContent();
        }

        [Authorize(Roles = "Chair")]
        [HttpDelete]
        public ActionResult<IsoCodeDto.ReadIsoCode> DeleteIsoCodes(IList<IsoCodeDto.ReadIsoCode> payload)
        {
            _isoCodeService.DeleteIsoCode(payload);
            return NoContent();
        }

        [Authorize(Roles = "Chair,Coordinator")]
        [HttpPut("transfer/{adminId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> TransferIsoCode(IList<IsoCodeDto.ReadIsoCode> payload, int adminId)
        {
            _isoCodeService.TransferIsocode(payload,adminId);
            return NoContent();
        }
    }
}