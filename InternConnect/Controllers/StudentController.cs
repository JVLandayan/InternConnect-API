using System;
using System.Collections.Generic;
using InternConnect.Dto.Student;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService student)
        {
            _studentService = student;
        }


        //GET /admin
        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto.ReadStudent>> GetAllStudent()
        {
            return Ok(_studentService.GetAll());
        }
        //GET /admin
        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpGet("dashboard")]
        public ActionResult<IEnumerable<StudentDto.ReadStudent>> GetAllStudentForDashboard()
        {
            return Ok(_studentService.GetAllForDashboard());
        }

        //GET /admin/id
        [Authorize(Roles = "Dean,Chair,Coordinator")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StudentDto.ReadStudent>> GetStudent(int id)
        {
            try
            {
                return Ok(_studentService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Student doesn't exist");
            }
        }


    }
}