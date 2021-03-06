using System.Collections.Generic;
using InternConnect.Dto.Company;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService company)
        {
            _companyService = company;
        }


        //GET /accounts
        [HttpGet("{id}", Name = "GetCompany")]
        public ActionResult<IEnumerable<CompanyDto.ReadCompany>> GetCompany(int id)
        {
            if (_companyService.GetById(id) != null) return Ok(_companyService.GetById(id));
            return BadRequest("Company doesn't exist");
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDto.ReadCompany>> GetAllCompany()
        {
            return Ok(_companyService.GetAllCompanies());
        }


        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPut]
        public ActionResult<CompanyDto.ReadCompany> UpdateCompany(CompanyDto.UpdateCompany payload)
        {
            _companyService.UpdateCompany(payload);
            return NoContent();
        }

        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPut("status")]
        public ActionResult<CompanyDto.ReadCompany> UpdateCompanyStatus(CompanyDto.UpdateCompanyStatus payload)
        {
            _companyService.UpdateCompanyStatus(payload);
            return NoContent();
        }


        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpDelete("{id}")]
        public ActionResult<CompanyDto.ReadCompany> DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id);
            return NoContent();
        }


        [Authorize(Roles = "Dean,Chair,Tech Coordinator")]
        [HttpPost]
        public ActionResult<CompanyDto.ReadCompany> AddCompany(CompanyDto.AddCompany payload)
        {
            var companyData = _companyService.AddCompany(payload);
            return CreatedAtRoute(nameof(GetCompany), new { companyData.Id }, companyData);
        }
    }
}