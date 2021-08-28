using System.Collections.Generic;
using InternConnect.Dto.Company;
using InternConnect.Service.Main;
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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CompanyDto.ReadCompany>> GetCompany(int id)
        {
            return Ok(_companyService.GetById(id));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDto.ReadCompany>> GetAllCompany()
        {
            return Ok(_companyService.GetAllCompanies());
        }


        //Coordinators
        //Authorize AuthCoordinatorClaim
        [HttpDelete("{id}")]
        public ActionResult DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id);
            return Ok();
        }

        //Authorize Student Claim
        // POST accounts/student 
        [HttpPut]
        public ActionResult<CompanyDto.ReadCompany> UpdateCompany(CompanyDto.UpdateCompany payload)
        {
            _companyService.UpdateCompany(payload);
            return Ok();
        }

        [HttpPost]
        public ActionResult<CompanyDto.ReadCompany> AddCompany(CompanyDto.AddCompany payload)
        {
            _companyService.AddCompany(payload);
            return Ok();
        }
        //Chairs


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

        //}
        //[Authorize]
        //[HttpDelete("{id}")]
        //public ActionResult DeleteMerch(int id)
        //{
        //    var photoFolderPath = _env.ContentRootPath + "/Photos/";
        //    var teamModelFromRepo = _repository.GetTeamById(id);
        //    if (teamModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _repository.DeleteTeam(teamModelFromRepo);
        //    System.IO.File.Delete(photoFolderPath + teamModelFromRepo.TeamsImage);
        //    _repository.SaveChanges();
        //    return NoContent();
        //}
    }
}