using System;
using System.Collections.Generic;
using InternConnect.Dto.PdfState;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Dean,Chair,Tech Coordinator,Coordinator")]
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

        [Authorize(Roles = "Dean")]
        [HttpPut]
        public ActionResult<PdfStateDto.ReadPdfState> UpdatePdfState(PdfStateDto.UpdatePdfState payload)
        {
            _pdfStateService.UpdatePdfState(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean")]
        [HttpPost]
        public ActionResult<PdfStateDto.ReadPdfState> AddPdfState(
            PdfStateDto.AddPdfState payload)
        {
            var pdfData = _pdfStateService.AddPdfState(payload);
            if (pdfData == null) return Ok();

            return BadRequest("PdfState existing");
        }
    }
}