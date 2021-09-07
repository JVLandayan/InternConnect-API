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
        [HttpGet]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAllAdmin()
        {
            return Ok(_adminService.GetAll());
        }

        //GET /admin/id
        [Authorize(Roles = "Dean")]
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AdminDto.ReadAdmin>> GetAdmin(int id)
        {
            try
            {
                return Ok(_adminService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Admin doesn't exist");
            }
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
    }
}