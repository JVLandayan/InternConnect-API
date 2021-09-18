using System.Collections.Generic;
using System.Linq;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(InternConnectContext context) : base(context)
        {
        }

        public IEnumerable<Student> GetAllStudentWithRelatedData()
        {
            return Context.Set<Student>().Include(s => s.Account).Include(s => s.Section).Include(s => s.Program).ToList();
        }
    }
}