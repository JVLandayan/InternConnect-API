using System;
using System.Collections.Generic;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Data.Repositories
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(InternConnectContext context) : base(context)
        {

        }



    }
}
