using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternConnect.Context;
using InternConnect.Data.Interfaces;
using InternConnect.Dto.Student;

namespace InternConnect.Service.Main
{
    public interface IStudentService
    {
        public StudentDto.ReadStudent GetById(int id);
        public IEnumerable<StudentDto.ReadStudent> GetAll();
        public IEnumerable<StudentDto.ReadStudent> GetAllForDashboard(string type, int id);
        public void UpdateStudentSection(StudentDto.UpdateStudent payload);
    }

    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly InternConnectContext _context;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository student, IMapper mapper, InternConnectContext context)
        {
            _studentRepository = student;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<StudentDto.ReadStudent> GetAll()
        {
            var studentList = _studentRepository.GetAllStudentWithRelatedData();
            var mappedList = new List<StudentDto.ReadStudent>();

            foreach (var student in studentList) mappedList.Add(_mapper.Map<StudentDto.ReadStudent>(student));

            return mappedList;
        }

        public IEnumerable<StudentDto.ReadStudent> GetAllForDashboard(string type, int id)
        {
            var studentList = _studentRepository.GetDataOfStudentsForDashboard().ToList();
            var mappedList = new List<StudentDto.ReadStudent>();

            foreach (var student in studentList) mappedList.Add(_mapper.Map<StudentDto.ReadStudent>(student));

            if (type == "program") return mappedList.Where(s => s.ProgramId == id);
            if (type == "section") return mappedList.Where(s => s.SectionId == id);

            if (type == "whole" && id == 0) return mappedList;

            return null;
        }

        public StudentDto.ReadStudent GetById(int id)
        {
            return _mapper.Map<StudentDto.ReadStudent>(_studentRepository.GetAllStudentWithRelatedData()
                .First(s => s.Id == id));
        }

        public void UpdateStudentSection(StudentDto.UpdateStudent payload)
        {
            var studentData = _studentRepository.Get(payload.Id);
            studentData.SectionId = payload.SectionId;
            _context.SaveChanges();
        }
    }
}