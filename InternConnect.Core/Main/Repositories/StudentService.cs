using System.Collections.Generic;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Student;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Service.Main.Repositories
{
    public interface IStudentService
    {
        public StudentDto.ReadStudent GetById(int id);
        public IEnumerable<StudentDto.ReadStudent> GetAll();
    }
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;

        public StudentService(IStudentRepository student, IMapper mapper, InternConnectContext context)
        {
            _studentRepository = student;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<StudentDto.ReadStudent> GetAll()
        {
            var studentList = _studentRepository.GetAll();
            var mappedList = new List<StudentDto.ReadStudent>();

            foreach (var student in studentList)
            {
                mappedList.Add(_mapper.Map<StudentDto.ReadStudent>(student));
            }

            return mappedList;
        }

        public StudentDto.ReadStudent GetById(int id)
        {
            return _mapper.Map<StudentDto.ReadStudent>(_studentRepository.Get(id));
        }
    }
}
