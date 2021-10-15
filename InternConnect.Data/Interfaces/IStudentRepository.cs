using System.Collections.Generic;
using InternConnect.Context.Models;

namespace InternConnect.Data.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        public IEnumerable<Student> GetAllStudentWithRelatedData();
        public IEnumerable<Student> GetDataOfStudentsForDashboard();

    }
}