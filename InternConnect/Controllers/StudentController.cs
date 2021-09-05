using System;
using System.Collections.Generic;
using InternConnect.Dto.Student;
using InternConnect.Service.Main;
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
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto.ReadStudent>> GetAllStudent()
        {
            return Ok(_studentService.GetAll());
        }

        //GET /admin/id
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