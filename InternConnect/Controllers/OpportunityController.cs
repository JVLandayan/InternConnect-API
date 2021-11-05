using System;
using System.Collections.Generic;
using InternConnect.Dto.Opportunity;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        public OpportunityController(IOpportunityService opportunity)
        {
            _opportunityService = opportunity;
        }


        //GET /accounts
        [HttpGet("{id}", Name = "GetOpportunity")]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetOpportunity(int id)
        {
            try
            {
                return Ok(_opportunityService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Opportunity doesn't exist");
            }
        }

        [HttpGet("company/{companyId}")]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetOpportunitiesForEachCompany(int companyId)
        {
            return Ok(_opportunityService.GetByCompanyId(companyId));
        }


        [HttpGet]
        public ActionResult<IEnumerable<OpportunityDto.ReadOpportunity>> GetAllOpportunity()
        {
            return Ok(_opportunityService.GetAllOpportunities());
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteOpportunity(int id)
        {
            _opportunityService.DeleteOpportunity(id);
            return Ok();
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPut]
        public ActionResult<OpportunityDto.ReadOpportunity> UpdateOpportunity(OpportunityDto.UpdateOpportunity payload)
        {
            _opportunityService.UpdateOpportunity(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPost]
        public ActionResult<OpportunityDto.ReadOpportunity> AddOpportunity(OpportunityDto.AddOpportunity payload)
        {
            var opportunityData = _opportunityService.AddOpportunity(payload);
            return CreatedAtRoute(nameof(GetOpportunity), new { opportunityData.Id }, opportunityData);
        }
    }
}