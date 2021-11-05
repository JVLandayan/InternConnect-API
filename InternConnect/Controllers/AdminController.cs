using System;
using System.Collections.Generic;
using InternConnect.Dto.Account;
using InternConnect.Dto.Admin;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService admin)
        {
            _adminService = admin;
        }


        //GET /admin
        [Authorize(Roles = "Dean")]
        [HttpGet("admins")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAllAdmin()
        {
            return Ok(_adminService.GetAll());
        }

        [Authorize(Roles = "Chair")]
        [HttpGet("coordinators/{programId}")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAllCoordinatorsByProgram(int programId)
        {
            return Ok(_adminService.GetAllCoordinatorByProgram(programId));
        }

        [Authorize(Roles = "Dean")]
        [HttpGet("chairs")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAllChairs()
        {
            return Ok(_adminService.GetAllChairByProgram());
        }

        [Authorize(Roles = "Dean")]
        [HttpGet("techcoord")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAllTechCoordinators()
        {
            return Ok(_adminService.GetAllTechCoordinators());
        }

        //GET /admin/id
        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAdmin(int id)
        {
            if (_adminService.GetById(id) != null) return Ok(_adminService.GetById(id));
            return BadRequest("Admin doesn't exist");
        }

        [Authorize(Roles = "Coordinator,Dean")]
        [HttpPut("signature/{id}")]
        public ActionResult<AccountDto.ReadAccount> UpdateSignature(AdminDto.UpdateAdmin payload, int id)
        {
            try
            {
                _adminService.UpdateAdmin(payload, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong");
            }
        }

        [Authorize(Roles = "Dean")]
        [HttpPut("program")]
        public ActionResult UpdateProgram(List<AdminDto.UpdateAdminProgram> payload)
        {
            _adminService.UpdateAdminProgram(payload[0], payload[1]);
            return NoContent();
        }


        [Authorize(Roles = "Chair")]
        [HttpPut("section")]
        public ActionResult UpdateSection(List<AdminDto.UpdateAdminSection> payload)
        {
            _adminService.UpdateAdminSection(payload[0], payload[1]);
            return NoContent();
        }

        [Authorize(Roles = "Coordinator,Dean")]
        [HttpDelete("esignature/{adminId}")]
        public ActionResult<string> DeleteSignature(int adminId, string adminEmail)
        {
            _adminService.DeleteESignature(adminId, adminEmail);
            return NoContent();
        }
    }
}