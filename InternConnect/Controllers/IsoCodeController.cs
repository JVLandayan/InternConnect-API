using System.Collections.Generic;
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
            if (isoCodeList != null) return Ok(isoCodeList);
            return BadRequest();
        }

        [Authorize(Roles = "Chair")]
        [HttpGet("program/{programId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> GetAllByProgram(int programId)
        {
            var isoCodeList = _isoCodeService.GetAllByProgramId(programId);
            if (isoCodeList != null) return Ok(isoCodeList);
            return BadRequest();
        }

        //[Authorize(Roles = "Chair")]
        //[HttpPost]
        //public ActionResult<IsoCodeDto.AddIsoCode> AddIsoCodes(IList<IsoCodeDto.AddIsoCode> payload)
        //{
        //    var isoCodeData = _isoCodeService.BulkAdd(payload);
        //    if (isoCodeData != null)
        //    {
        //        return BadRequest($"{isoCodeData.Code} already exists. Please try again");
        //    }
        //    return NoContent();

        //}

        [Authorize(Roles = "Chair")]
        [HttpDelete("{id}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> DeleteIsoCodes(int id)
        {
            _isoCodeService.DeleteIsoCode(id);
            return NoContent();
        }

        [Authorize(Roles = "Chair")]
        [HttpPut("transfertocoordinator/{adminId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> TransferIsoCodeToCoordinator(IsoCodeDto.TransferIsoCode payload,
            int adminId)
        {
            _isoCodeService.TransferIsocodeToCoordinator(payload, adminId);
            return NoContent();
        }

        [Authorize(Roles = "Chair")]
        [HttpPost("addisocode/{adminId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> AddIsoCodeToCoordinator(IList<IsoCodeDto.AddIsoCode> payload,
            int adminId)
        {
            var isoCodeData = _isoCodeService.AddIsocodeToCoordinator(payload, adminId);
            if (isoCodeData != null) return BadRequest($"Iso-Code {isoCodeData.Code} already exists. Please try again");
            return NoContent();
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPut("transfertochair/{programId}")]
        public ActionResult<IsoCodeDto.ReadIsoCode> TransferIsoCodeToChair(IList<IsoCodeDto.TransferIsoCode> payload,
            int programId)
        {
            _isoCodeService.TransferIsocodeToChair(payload, programId);
            return NoContent();
        }
    }
}