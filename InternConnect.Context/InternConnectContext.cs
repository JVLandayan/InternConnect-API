using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace InternConnect.Context
{
    public class InternConnectContext : DbContext
    {
        public InternConnectContext(DbContextOptions<InternConnectContext> opt) : base(opt)
        {

        }
        public DbSet<AcademicYear> AcademicYear { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<PdfState> PdfState { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<WebState> WebState { get; set; }

    }
}
